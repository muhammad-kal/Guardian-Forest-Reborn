using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Mathematics;
using UnityEngine.UIElements;

public class SpotPohon : MonoBehaviour
{
    [Header("Action")]
    public static Action<SpotPohon> ActiondalamAreaPohon;

    [Header("Elements")]
    [SerializeField] private GameObject pohonAsli;
    [SerializeField] private GameObject pohonGhaib;
    [SerializeField] private SpotPohonManager spotPohonManager;
    [SerializeField] private GameObject apiPrefab;

    [Header("Settings")]
    private float ukuranRandom;


    public enum stateSpotPohon { TidakTanam, Tanam,  }
    private stateSpotPohon stateSaatIni;
    public bool diLokasi = false;
    private bool isterbakar = false;
    private bool isSudahTumbuh = false;
    [SerializeField] private Transform titikApi;



    private void Start()
    {
        pohonAsli.SetActive(false);
        pohonGhaib.SetActive(false);
        stateSaatIni = stateSpotPohon.TidakTanam;
        ukuranRandom = UnityEngine.Random.Range(.5f, 1f);
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; // agar tidak jatuh

    }

    public void masukLokasiTanam(Collider other)
    {
        if (other.gameObject.tag == "Player" && stateSaatIni == stateSpotPohon.TidakTanam)
        {
            diLokasi = true;
            pohonGhaib.SetActive(true);
            ActiondalamAreaPohon?.Invoke(this);
        }
    }

    public void keluarLokasiTanam(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pohonGhaib.SetActive(false);
        }
        diLokasi = false;
    }

    public void Terbakar()
    {
        if (!isterbakar)
        {
            Vector3 posisiApi = GetPosisiApiDiAtasPohon();
            Debug.Log(posisiApi);
            Instantiate(apiPrefab, posisiApi, Quaternion.identity, transform);
            isterbakar = true;
        }
    }

    private Vector3 GetTinggiPohon(Vector3 pos)
    {
        ukuranRandom = UnityEngine.Random.Range(0.5f, 4f);
        pos.y = ukuranRandom; // hanya ubah Y, sisanya tetap
        return pos;
    }
    private Vector3 GetPosisiApiDiAtasPohon()
    {
        // Ambil tinggi asli dari mesh
        float tinggiMesh = pohonAsli.GetComponentInChildren<Renderer>().bounds.size.y;

        // Ambil posisi dasar pohon
        Vector3 posisiDasar = pohonAsli.transform.position;

        // Tambahkan tinggi aktual ke sumbu Y
        posisiDasar.y = tinggiMesh-8;

        return posisiDasar;
    }

    public void Tertanam()
    {
        if (diLokasi)
        {
            pohonGhaib.SetActive(false);
            pohonAsli.SetActive(true);
            stateSaatIni = stateSpotPohon.Tanam;
            isSudahTumbuh = true;
            Tumbuh();
        }
    }

    private void Tumbuh()
    {
        pohonAsli.gameObject.LeanScale(Vector3.one * 0.1f, 1f).setEase(LeanTweenType.easeInOutBack).setOnComplete(()
            => pohonAsli.gameObject.LeanScale(Vector3.one * ukuranRandom, 10f));
        
    }
}
