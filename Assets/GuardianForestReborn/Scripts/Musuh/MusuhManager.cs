using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusuhManager : MonoBehaviour
{
    [SerializeField] private GameObject musuhPrefab;
    [SerializeField] private int MaxMusuh = 2;
    [SerializeField] private Transform SpawnPoint1;
    [SerializeField] private Transform SpawnPoint2;

    private SpotPohonManager spotPohonManager;
    private int spawnIndex = 0;
    public bool BolehSpawn = false;
    public int MaxMusuhDiLevel;
    private int JumlahMusuhSekarang;
    private JumlahMusuh UIJumlahMusuh;

    private void Start()
    {
        spotPohonManager = FindAnyObjectByType<SpotPohonManager>();
        StartCoroutine(SpawnerLoop());
        UIJumlahMusuh = FindObjectOfType<JumlahMusuh>();
    }
    public void KeperluanLevel(int max, int maxSpawnSameTime)
    {
        MaxMusuhDiLevel = max;
        MaxMusuh = maxSpawnSameTime;
        BolehSpawn = true;
        UIJumlahMusuh.SetNilai(0, MaxMusuhDiLevel);
    }

    private IEnumerator SpawnerLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            if (!BolehSpawn) continue;

            if (JumlahMusuhSekarang >= MaxMusuhDiLevel)
            {
                yield return null;
                Level root = transform.root.GetComponent<Level>();
                root.GameSelesai();
                continue;
            }

            if (GetJumlahMusuhAktif() < MaxMusuh)
            {
                Transform spawnPoint = (spawnIndex % 2 == 0) ? SpawnPoint1 : SpawnPoint2;
                spawnIndex++;

                SpotPohon targetSpot = spotPohonManager?.GetTargetMusuh(spawnPoint.position);

                if (targetSpot != null)
                {
                    SpawnMusuh(spawnPoint.position, targetSpot);
                    JumlahMusuhSekarang++;
                    UIJumlahMusuh.UpdateNilai(1);
                }
            }
        }
    }

    private void SpawnMusuh(Vector3 posisiSpawn, SpotPohon targetSpot)
    {
        GameObject musuhBaru = Instantiate(musuhPrefab, posisiSpawn, Quaternion.identity, this.transform); // jadi anak dari manager
        Musuh musuhScript = musuhBaru.GetComponent<Musuh>();
        if (musuhScript != null)
        {
            musuhScript.SetTarget(targetSpot.transform); // arahkan musuh ke target pohon
            targetSpot.MenargetkanAnda(musuhBaru); // beri tahu pohon bahwa musuh datang
            Debug.Log("a");
        }
    }

    private int GetJumlahMusuhAktif()
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Musuh"))
                count++;
        }
        return count;
    }
}
