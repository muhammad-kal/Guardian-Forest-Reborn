using System.Collections;
using UnityEngine;

public class MusuhManagerTutorial : MonoBehaviour
{
    [SerializeField] private GameObject musuhPrefab;
    [SerializeField] private int MaxMusuh = 5;
    [SerializeField] private Transform SpawnPoint1;
    [SerializeField] private Transform SpawnPoint2;

    private SpotPohonManagerTutorial SpotPohonManagerTutorial;
    private int spawnIndex = 0;

    private void Start()
    {
        SpotPohonManagerTutorial = FindAnyObjectByType<SpotPohonManagerTutorial>();
        // Tidak ada lagi SpawnerLoop di Start
    }

    private void SpawnMusuh(Vector3 posisiSpawn, SpotPohonTutorial targetSpotTutorial)
    {
        GameObject musuhBaru = Instantiate(musuhPrefab, posisiSpawn, Quaternion.identity, this.transform); // jadi anak dari manager
        MusuhTutorial musuhScript = musuhBaru.GetComponent<MusuhTutorial>();
        if (musuhScript != null && targetSpotTutorial != null)
        {
            musuhScript.SetTarget(targetSpotTutorial.transform); // arahkan musuh ke target pohon
            targetSpotTutorial.MenargetkanAnda(musuhBaru);       // beri tahu pohon bahwa musuh datang
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

    public void spawnsekali()
    {
        if (GetJumlahMusuhAktif() >= MaxMusuh) return;

        Transform spawnPoint = (spawnIndex % 2 == 0) ? SpawnPoint1 : SpawnPoint2;
        spawnIndex++;

        SpotPohonTutorial targetSpot = SpotPohonManagerTutorial?.GetTargetMusuh(spawnPoint.position);
        if (targetSpot != null)
        {
            SpawnMusuh(spawnPoint.position, targetSpot);
        }
    }
}
