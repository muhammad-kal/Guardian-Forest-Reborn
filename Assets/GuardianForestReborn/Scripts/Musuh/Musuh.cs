using UnityEngine;

public class Musuh : MonoBehaviour
{
    private Transform target;
    private Vector3 spawnPoint; // simpan posisi, bukan Transform
    private bool sedangDespawn = false;

    [SerializeField] private float radiusSampaiTarget = 0.1f;
    private SpotPohonManager spotPohonManager;

    void Start()
    {
        spawnPoint = transform.position; // perbaikan: simpan posisi awal saat musuh muncul
        spotPohonManager = FindAnyObjectByType<SpotPohonManager>();
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
            transform.position = Vector3.MoveTowards(posisiSekarang, posisiTarget, 3f * Time.deltaTime);

            float jarak = Mathf.Abs(transform.position.x - posisiTarget.x);
            if (jarak <= radiusSampaiTarget)
            {
                if (!sedangDespawn)
                {
                    // Sampai ke target utama, stop
                    target = null;
                    AktifkanColliderApi();
                }
                else
                {
                    // Sampai ke posisi despawn, hapus objek
                    Destroy(gameObject);
                }
            }
        }
    }

    public void GantiTarget()
    {
        NonaktifkanColliderApi();
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
        Debug.Log("a");
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
}
