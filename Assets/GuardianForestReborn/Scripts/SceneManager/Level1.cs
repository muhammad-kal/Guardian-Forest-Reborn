using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Jeda Sebelum Game Dimulai")]
    [SerializeField] private float GameStart;
    [Header("Waktu Sebelum Terbakar")]
    [SerializeField] private float FireSpawn;
    [Header("Max Musuh Di Level")]
    [SerializeField] private int EnemyMaxSpawn;
    [Header("Max Musuh Pada Saat Bersamaan")]
    [SerializeField] private int EnemyMaxSpawnSameTime;
    [Header("Darah Pohon Tumbuh (Tanam hanya setengahnya)")]
    [SerializeField] private float DarahPohon;

    MusuhManager musuhManager;
    SpotPohonManager spotPohonManager;
    SaveData data;
    private bool End = false;

    public void Start()
    {
        musuhManager = FindObjectOfType<MusuhManager>();
        spotPohonManager = FindObjectOfType<SpotPohonManager>();
        data = SaveManager.Load();
        data.ResetToDefault();

        DeklarasiKeChild();
    }
    private void DeklarasiKeChild()
    {
        tanpadelay();
        StartCoroutine(Delaybasic());
        StartCoroutine(DelayGame());
    }
    private void tanpadelay()
    {

    }
    IEnumerator Delaybasic()
    {
        yield return new WaitForSeconds(1f);
        spotPohonManager.KeperluanLevel(FireSpawn, DarahPohon, data);
    }
    IEnumerator DelayGame()
    {
        yield return new WaitForSeconds(GameStart);
        Debug.Log("Game Dimulai");
        musuhManager.BolehSpawn = true;
        musuhManager.KeperluanLevel(EnemyMaxSpawn, EnemyMaxSpawnSameTime);
        spotPohonManager.Gamestart();
    }
    public void GameSelesai()
    {
        if (!End)
        {
            End = true;
            Debug.Log("Game Selesai");
            SaveManager.Save(data);
        }
    }
}
