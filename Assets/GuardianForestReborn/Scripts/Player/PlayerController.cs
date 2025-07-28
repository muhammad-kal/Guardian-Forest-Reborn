using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    private string alat;
    private string LokasiSaatIni;



    [Header("Settings")]
    [SerializeField] private float kecepatan;

    public bool actionActive = false;



    private void Start()
    {
        karakterKontroller = GetComponent<CharacterController>();
        playerAlatSelector = GetComponent<PlayerAlatSelctor>();
        playerSkillMenanam = GetComponent<PlayerSkillMenanam>();
        playerSkillMenyiram = GetComponent<PlayerSkillMenyiram>();

        playerAlatSelector.actionPilihAlat += AlatTerpilihCallback;

    }
    private void OnDestroy()
    {
        playerAlatSelector.actionPilihAlat -= AlatTerpilihCallback;

    }
    private void AlatTerpilihCallback(PlayerAlatSelctor.Alat alatTerpilih)
    {
        actionActive = false;
        alat = alatTerpilih.ToString();
    }

    private void Update()
    {
        MovementManager();
    }

    private void MovementManager()
    {
        Vector3 pergerakan = new Vector3(analog.GetBergerak.x * kecepatan * Time.deltaTime / Screen.width, transform.position.y, transform.position.z);
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
        playerAnimator.AnimasiManager(pergerakan);
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
                playerSkillMenanam.MenanamPohon(other.GetComponentInParent<SpotPohon>());
            }
            else if (other.gameObject.name == "Ladang")
            {
                playerSkillMenanam.Menanam(other.GetComponent<LadangManager>());
            }
        }
        else if (alat == "Air")
        {
            if (other.gameObject.name == "TanamZone")
            {
                playerSkillMenyiram.MenyiramApi(other.GetComponentInParent<SpotPohon>());
            }
            else if (other.gameObject.name == "Ladang")
            {
                playerSkillMenyiram.Menyiram(other.GetComponent<LadangManager>());
            }
        }
    }

}
