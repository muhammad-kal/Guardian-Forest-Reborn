using UnityEngine;
using UnityEngine.SceneManagement;

public class Musuh : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Animator pembakarAnimatorController;
    [SerializeField] private Transform pembakarRenderer;


    private Transform target;
    private Vector3 spawnPoint; // simpan posisi, bukan Transform
    private bool sedangDespawn = false;

    [SerializeField] private float radiusSampaiTarget = 0.1f;
    private SpotPohonManager spotPohonManager;
    private float kecepatan = 3f;

    void Start()
    {
        spawnPoint = transform.position; // perbaikan: simpan posisi awal saat musuh muncul
        spotPohonManager = FindAnyObjectByType<SpotPohonManager>();
        pembakarAnimatorController.Play("Jalan");
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        sedangDespawn = false;
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 posisiSekarang = transform.position;
            Vector3 posisiTarget = new Vector3(target.position.x, posisiSekarang.y, posisiSekarang.z);
            transform.position = Vector3.MoveTowards(posisiSekarang, posisiTarget, kecepatan * Time.deltaTime);
            pembakarRenderer.forward = (posisiTarget.x - posisiSekarang.x > 0) ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);


            float jarak = Mathf.Abs(transform.position.x - posisiTarget.x);
            if (jarak <= radiusSampaiTarget)
            {
                if (!sedangDespawn)
                {
                    // Sampai ke target utama, stop
                    target = null;
                    pembakarAnimatorController.speed = 0f; // Pause Animasi
                    HadapKeDepan();
                    AktifkanColliderApi();
                }
                else
                {
                    // Sampai ke posisi despawn, hapus objek
                    if (SceneManager.GetActiveScene().name == "Tutorial")
                    {
                        Destroy(transform.parent.gameObject);
                    }     
                    Destroy(gameObject);
                }
            }
        }
    }
    private void HadapKeDepan()
    {
        pembakarRenderer.forward = Vector3.forward;
    }

    public void GantiTarget()
    {
        NonaktifkanColliderApi();
        pembakarAnimatorController.speed = 1f; // Mulai lagi animasi
        SpotPohon targetSpot = spotPohonManager.GetTargetMusuh(transform.position);
        if (targetSpot)
        {
            targetSpot.MenargetkanAnda(gameObject);
            SetTarget(targetSpot.transform);
        }
        else
            despawn();
    }

    public void despawn()
    {
        // Buat transform target dummy untuk posisi vector
        GameObject dummy = new GameObject("DespawnTarget");
        dummy.transform.position = spawnPoint;
        target = dummy.transform;
        sedangDespawn = true;
    }

    public void AktifkanColliderApi()
    {
        Transform apiChild = transform.Find("Api");
        if (apiChild != null)
        {
            Collider colliderApi = apiChild.GetComponent<Collider>();
            if (colliderApi != null)
            {
                colliderApi.enabled = true;
            }
        }
    }
    public void NonaktifkanColliderApi()
    {
        Transform apiChild = transform.Find("Api");
        if (apiChild != null)
        {
            Collider colliderApi = apiChild.GetComponent<Collider>();
            if (colliderApi != null)
            {
                colliderApi.enabled = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            despawn();
            kecepatan = 10f;
        }
    }
}
