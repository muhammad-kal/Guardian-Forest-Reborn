using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Jeda Sebelum Musuh Muncul")]
    [SerializeField] private float EnemySpawn;
    [Header("Waktu Sebelum Terbakar")]
    [SerializeField] private float FireSpawn;
    [Header("Max Musuh Di Level")]
    [SerializeField] private int EnemyMaxSpawn;
    [Header("Max Musuh Pada Saat Bersamaan")]
    [SerializeField] private int EnemyMaxSpawnSameTime;

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
        StartCoroutine(DelaySpawnMusuh());
    }
    private void tanpadelay()
    {

    }
    IEnumerator Delaybasic()
    {
        yield return new WaitForSeconds(1f);
        spotPohonManager.KeperluanLevel(FireSpawn);
        spotPohonManager.SetData(data);
    }
    IEnumerator DelaySpawnMusuh()
    {
        yield return new WaitForSeconds(EnemySpawn);
        musuhManager.BolehSpawn = true;
        musuhManager.KeperluanLevel(EnemyMaxSpawn, EnemyMaxSpawnSameTime);
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
