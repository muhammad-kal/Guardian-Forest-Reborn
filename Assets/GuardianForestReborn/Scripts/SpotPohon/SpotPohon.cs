using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Mathematics;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class SpotPohon : MonoBehaviour
{
    [Header("Action")]
    public static Action<SpotPohon> ActiondalamAreaPohon;

    [Header("Elements")]
    [SerializeField] private GameObject pohonAsli;
    [SerializeField] private GameObject pohonGhaib;
    [SerializeField] private SpotPohonManager spotPohonManager;
    [SerializeField] private GameObject apiPrefab;
    [SerializeField] private DarahPohon darahPohon;

    [Header("Settings")]
    private float ukuranRandom;
    [SerializeField] private float tinggiCustom;


    public enum stateSpotPohon { TidakTanam, Tanam, Tumbuh }
    private stateSpotPohon stateSaatIni;
    public bool diLokasi = false;
    private bool isterbakar = false;
    public bool sudahDeteksiApi = false;
    private List<GameObject> musuhYangMenarget = new List<GameObject>();
    [SerializeField] private Transform titikApi;
    private GameObject ApiChild;
    private void Start()
    {
        // Inisialisasi nilai awal (berlaku untuk semua scene)
        tinggiCustom = UnityEngine.Random.Range(2, 5);
        ukuranRandom = UnityEngine.Random.Range(0.5f, 1f);
        pohonAsli.SetActive(false);
        pohonGhaib.SetActive(false);
        stateSaatIni = stateSpotPohon.TidakTanam;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; // agar tidak jatuh
    }
    private void Update()
    {
        Terluka();
    }
    private void Terluka()
    {
        if (isterbakar)
        {
            darahPohon.kurangiDarah();
        }
    }
    public void DarahHabis()
    {
        isterbakar = false;
        Destroy(ApiChild);
        spotPohonManager.GantiStatePohon(gameObject.name, "Tidak Tanam");
        ResetKeTanam();
    }
    private void ResetKeTanam()
    {
        pohonGhaib.SetActive(false);
        pohonAsli.SetActive(false);
        pohonAsli.transform.localScale = Vector3.zero; // atau Vector3.one * ukuranAwal jika ada
        stateSaatIni = stateSpotPohon.TidakTanam;
    }
    public void startTanam()
    {
        Tanam();
    }
    public void startTumbuh()
    {
        pohonGhaib.SetActive(true);
        pohonAsli.SetActive(true);
        pohonAsli.transform.localScale = Vector3.one * ukuranRandom;
        stateSaatIni = stateSpotPohon.Tumbuh;
        pohonGhaib.SetActive(false);
    }

    public void masukLokasiTanam(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (other.GetComponent<PlayerController>().alat == "Bibit")
        {
            if (other.gameObject.tag == "Player" && other.GetComponent<PlayerController>().alat == "Bibit" && stateSaatIni == stateSpotPohon.TidakTanam)
            {
                diLokasi = true;
                pohonGhaib.SetActive(true);
                ActiondalamAreaPohon?.Invoke(this);
            }
        }
        else
        {
            pohonGhaib.SetActive(false);
        }
        if (other.gameObject.tag == "Player" && (stateSaatIni == stateSpotPohon.Tumbuh))
        {
            darahPohon.gameObject.SetActive(true);
            diLokasi = true;
        }
    }

    public void keluarLokasiTanam(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pohonGhaib.SetActive(false);
            darahPohon.gameObject.SetActive(false);
        }
        diLokasi = false;
    }

    public void Terbakar()
    {
        if (!isterbakar)
        {
            Debug.Log("fireee");
            Vector3 posisiApi = GetPosisiApiDiAtasPohon();
            Debug.Log(posisiApi);
            ApiChild = Instantiate(apiPrefab, posisiApi, Quaternion.identity, transform);
            isterbakar = true;
            PerintahkanSemuaMusuhGantiTarget();
        }
    }
    private Vector3 GetPosisiApiDiAtasPohon()
    {
        // Ambil tinggi asli dari mesh
        float tinggiMesh = pohonAsli.transform.position.y + tinggiCustom;

        // Ambil posisi dasar pohon
        Vector3 posisiDasar = pohonAsli.transform.position;

        // Tambahkan tinggi aktual ke sumbu Y
        posisiDasar.y = tinggiMesh;

        return posisiDasar;
    }

    public void Tertanam()
    {
        if (diLokasi)
        {
            pohonGhaib.SetActive(false);
            pohonAsli.SetActive(true);
            stateSaatIni = stateSpotPohon.Tanam;
            Tanam();
        }
    }
    public bool SiapDisiram()
    {
        return stateSaatIni == stateSpotPohon.Tanam || isterbakar;
    }
    public bool SiapDitanam()
    {
        return stateSaatIni == stateSpotPohon.TidakTanam;
    }

    public void Disiram()
    {
        if (isterbakar)
            MatikanApi();
        else
        {
            if (diLokasi && stateSaatIni == stateSpotPohon.Tanam)
            {
                Tumbuh();
            }
        }
    }


    private void MatikanApi()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Api"))
            {
                sudahDeteksiApi = false;
                Destroy(child.gameObject);
                break;
            }
        }
        isterbakar = false;
    }

    public void Tanam()
    {
        pohonAsli.transform.localScale = Vector3.one * 0.1f;
        spotPohonManager.GantiStatePohon(gameObject.name, "Tanam");
        Debug.Log("tanam");
    }

    public void Tumbuh()
    {
        pohonAsli.gameObject.LeanScale(Vector3.one * ukuranRandom, 10f)
            .setEase(LeanTweenType.easeInOutBack);
        stateSaatIni = stateSpotPohon.Tumbuh;
        spotPohonManager.GantiStatePohon(gameObject.name, "Tumbuh");
        Debug.Log("tumbuh");
    }
    public bool IsValidTarget()
    {
        return !isterbakar && (stateSaatIni == stateSpotPohon.Tanam || stateSaatIni == stateSpotPohon.Tumbuh);
    }
    public void MenargetkanAnda(GameObject musuh)
    {
        if (!musuhYangMenarget.Contains(musuh))
        {
            musuhYangMenarget.Add(musuh);
            Debug.Log($"{musuh.name} menarget pohon {name}");
        }
    }
    public void PerintahkanSemuaMusuhGantiTarget()
    {
        for (int i = musuhYangMenarget.Count - 1; i >= 0; i--)
        {
            GameObject musuh = musuhYangMenarget[i];

            if (musuh != null)
            {
                Musuh musuhScript = musuh.GetComponent<Musuh>();
                if (musuhScript != null)
                {
                    musuhScript.GantiTarget();
                }
            }
        }
        // Bersihkan list setelah semua diperintahkan Despawn
        musuhYangMenarget.Clear();
    }

    public bool stateBisaTerbakar()
    {
        return stateSaatIni == stateSpotPohon.Tanam || stateSaatIni == stateSpotPohon.Tumbuh;
    }
    public bool apakahTerbakar()
    {
        return isterbakar;
    }
    public void WaktuSebelumTerbakar(float FireSpawn)
    {
        ApiTrigger[] semuaApi = GetComponentsInChildren<ApiTrigger>();

        foreach (var api in semuaApi)
        {
            api.waktuTunggu = FireSpawn; // atau sesuai logic kamu
        }
    }
}
