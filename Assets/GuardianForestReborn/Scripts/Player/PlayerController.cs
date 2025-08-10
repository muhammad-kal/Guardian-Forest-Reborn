using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private KontrollerMobile analog;
    [SerializeField] private PlayerAnimator playerAnimator;
    public ParticleSystem[] semuaPartikel;

    private CharacterController karakterKontroller;
    private PlayerAlatSelctor playerAlatSelector;
    private PlayerSkillMenanam playerSkillMenanam;
    private PlayerSkillMenyiram playerSkillMenyiram;
    private InventoryTutorial inventory;

    public string alat;
    private string LokasiSaatIni;
    private Transform TargetOtomatis;
    private bool jalanotomatis = false;
    private string currentSceneName;



    [Header("Settings")]
    [SerializeField] private float kecepatan;

    public bool actionActive = false;



    private void Start()
    {
        karakterKontroller = GetComponent<CharacterController>();
        playerAlatSelector = GetComponent<PlayerAlatSelctor>();
        playerSkillMenanam = GetComponent<PlayerSkillMenanam>();
        playerSkillMenyiram = GetComponent<PlayerSkillMenyiram>();
        Inventory inventory = GetComponent<Inventory>();
        InventoryTutorial inventoryTutorial = GetComponent<InventoryTutorial>();

        if (inventory != null)
        {
            inventory.onItemChanged = HandleItemChange;
        }
        else if (inventoryTutorial != null)
        {
            inventoryTutorial.onItemChanged = HandleItemChange;
        }


        // playerAlatSelector.actionPilihAlat += AlatTerpilihCallback;
        GameObject rootObject = transform.root.gameObject;

        currentSceneName = SceneManager.GetActiveScene().name;
    }
    void HandleItemChange(string item)
    {
        actionActive = false;
        alat = item;
    }
    // private void OnDestroy()
    // {
    //     playerAlatSelector.actionPilihAlat -= AlatTerpilihCallback;

    // }
    // private void AlatTerpilihCallback(PlayerAlatSelctor.Alat alatTerpilih)
    // {
    //     actionActive = false;
    //     alat = alatTerpilih.ToString();
    // }

    private void Update()
    {
        MovementManager();
    }

    private void MovementManager()
    {
        if (jalanotomatis && (Vector3.Distance(transform.position, TargetOtomatis.position) >= 2f))
        {
            Jalanotomatis();
        }
        else if (jalanotomatis && Vector3.Distance(transform.position, TargetOtomatis.position) < 2f)
        {
            BerhentiJalanOtomatis();
        }
        else
        {
            Vector3 pergerakan = new Vector3(analog.GetBergerak.x * kecepatan * Time.deltaTime / Screen.width, transform.position.y, transform.position.z);
            //Vector3 pergerakan = new Vector3(analog.GetBergerak.x * kecepatan * Time.deltaTime, 0, 0);
            pergerakan.y = 0;
            pergerakan.z = 0;
            pergerakan.x = Mathf.Clamp(pergerakan.x, -0.12f, 0.12f);

            if (pergerakan.x > 0)
                {
                    Flip(90, semuaPartikel);
                }
                else if (pergerakan.x < 0)
                {
                    Flip(270, semuaPartikel);
                }
            karakterKontroller.Move(pergerakan);
            playerAnimator.AnimasiManager(pergerakan);
            jalanotomatis = false;
            TargetOtomatis = null;
        }
    }

    public void Flip(float arah, ParticleSystem[] partikelTerkait)
    {
        float kanan = 90;
        float kiri = 270;
        float depan = 0;
        // float posisiZPartikel = isKanan ? 0.74f : -0.74f;
        Transform childRender = gameObject.transform.GetChild(0);
        for (int i = 0; i < partikelTerkait.Length; i++)
        {
            if (arah == 90)
            {
                childRender.rotation = Quaternion.Euler(0, kanan, 0);

                partikelTerkait[i].transform.rotation = (Quaternion.Euler(0, 60, 0));
                Vector3 pos = partikelTerkait[i].transform.localPosition;
                pos.x = Mathf.Abs(pos.x); // pastikan di kanan
                partikelTerkait[i].transform.localPosition = pos;
            }
            else if (arah == 270)
            {
                childRender.rotation = Quaternion.Euler(0, kiri, 0);
                partikelTerkait[i].transform.rotation = (Quaternion.Euler(0, -60, 0));
                Vector3 pos = partikelTerkait[i].transform.localPosition;
                pos.x = -Mathf.Abs(pos.x); // pastikan di kiri
                partikelTerkait[i].transform.localPosition = pos;
            }
            else
            {
                childRender.rotation = Quaternion.Euler(0, depan, 0);
                partikelTerkait[i].transform.rotation = (Quaternion.Euler(0, 0, 0));
                Vector3 pos = partikelTerkait[i].transform.localPosition;
                pos.x = -Mathf.Abs(pos.x);
                partikelTerkait[i].transform.localPosition = pos;
            }
        }
    }
    public void JalanKeTargetX(Transform target)
    {
        jalanotomatis = true;
        TargetOtomatis = target;
    }
    private void BerhentiJalanOtomatis()
    {
        jalanotomatis = false;
        TargetOtomatis = null;
        // transform.root.GetComponent<Tutorial>()?.NextStep();
    }
    private void Jalanotomatis()
    {
        Vector3 pergerakan = new Vector3(0.2f, 0, 0);
        pergerakan.y = 0;
        pergerakan.z = 0;

        if (pergerakan.x > 0)
        {
            Flip(90, semuaPartikel);
        }
        else if (pergerakan.x < 0)
        {
            Flip(270, semuaPartikel);
        }
        karakterKontroller.Move(pergerakan);
        playerAnimator.AnimasiManagerOtomatis(pergerakan);
    }
    public void ubahArahMenanam()
    {
        Flip(0, semuaPartikel);
    }
    public void Action()
    {
        actionActive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        actionActive = false;
        LokasiSaatIni = other.gameObject.name;
    }
    private void OnTriggerExit(Collider other)
    {
        actionActive = false;
        LokasiSaatIni = "";
        stopAnimation();
    }
    public string lokasiSaatIni()
    {
        return LokasiSaatIni;
    }
    private void stopAnimation()
    {
        playerSkillMenyiram.BerhentiMenyiram();
    }
    private void OnTriggerStay(Collider other)
    {
        if (alat == "Bibit")
        {
            if (other.gameObject.name == "TanamZone")
            {
                if (currentSceneName == "Level 1 - Tutorial")
                    playerSkillMenanam.MenanamPohonTutorial(other.GetComponentInParent<SpotPohonTutorial>());
                else
                    playerSkillMenanam.MenanamPohon(other.GetComponentInParent<SpotPohon>());
            }
            else if (other.gameObject.name == "TanahLadangZone")
            {
                playerSkillMenanam.Menanam(other.GetComponentInParent<LadangManager>());
            }
        }
        else if (alat == "Gembor")
        {
            if (other.gameObject.name == "TanamZone")
            {
                if (currentSceneName == "Level 1 - Tutorial")
                    playerSkillMenyiram.MenyiramApiTutorial(other.GetComponentInParent<SpotPohonTutorial>());
                else
                    playerSkillMenyiram.MenyiramApi(other.GetComponentInParent<SpotPohon>());
            }
            else if (other.gameObject.name == "TanahLadangZone")
            {
                playerSkillMenyiram.Menyiram(other.GetComponentInParent<LadangManager>());
            }
        }
    }

}
