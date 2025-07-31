using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Waktu Sebelum Musuh Muncul")]
    [SerializeField] private float EnemySpawn;
    [Header("Waktu Sebelum Terbakar")]
    [SerializeField] private float FireSpawn;
    [Header("Max Musuh Di Level")]
    [SerializeField] private int EnemyMaxSpawn;

    MusuhManager musuhManager;
    SpotPohonManager spotPohonManager;
    SaveData data;

    public void Start()
    {
        musuhManager = FindObjectOfType<MusuhManager>();
        spotPohonManager = FindObjectOfType<SpotPohonManager>();
        data = SaveManager.Load();

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
        musuhManager.MaxMusuhLevel = EnemyMaxSpawn;
    }
    IEnumerator Delaybasic()
    {
        yield return new WaitForSeconds(1f);
        spotPohonManager.KeperluanLevel(FireSpawn);
        spotPohonManager.SetData(data.listEntity);
    }
    IEnumerator DelaySpawnMusuh()
    {
        yield return new WaitForSeconds(EnemySpawn);
        musuhManager.BolehSpawn = true;
    }
    public void MaxMusuhTercapai()
    {
        Debug.Log("Game Selesai");
    }
}
