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
    private bool bolehupgrade = true;
    private List<GameObject> musuhYangMenarget = new List<GameObject>();
    [SerializeField] private Transform titikApi;
    private GameObject ApiChild;
    private Vector3 originalScale;
    private float Mydarah;
    private void Start()
    {
        tinggiCustom = UnityEngine.Random.Range(2, 5);
        ukuranRandom = UnityEngine.Random.Range(0.3f, 0.6f);
        pohonAsli.SetActive(false);
        pohonGhaib.SetActive(false);
        stateSaatIni = stateSpotPohon.TidakTanam;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; // agar tidak jatuh
        originalScale = pohonAsli.transform.localScale;
    }
    public void Gamestart()
    {
        bolehupgrade = false;
    }
    public void SetDarah(float DarahBaru)
    {
        Mydarah = DarahBaru;
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
        if (stateSaatIni == stateSpotPohon.Tumbuh)
        {
            Mydarah /= 2;
            darahPohon.setMaxDarah(Mydarah);
        }
        isterbakar = false;
        sudahDeteksiApi = false;
        Destroy(ApiChild);
        spotPohonManager.GantiStatePohon(gameObject.name, "Tidak Tanam");
        ResetKeTanam();
        darahPohon.gameObject.SetActive(false);
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
        pohonAsli.SetActive(true);
        stateSaatIni = stateSpotPohon.Tanam;
        Tanam();
        darahPohon.setMaxDarah(Mydarah);
    }
    public void startTumbuh()
    {
        pohonAsli.SetActive(true);
        pohonGhaib.SetActive(false);
        Tumbuh();
        Mydarah /= 2;
        darahPohon.setMaxDarah(Mydarah);
    }

    public void masukLokasiTanam(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        diLokasi = true;
        string alatSekarang = other.GetComponent<PlayerController>().alat;

        if (stateSaatIni == stateSpotPohon.Tumbuh || stateSaatIni == stateSpotPohon.Tanam)
        {
            darahPohon.gameObject.SetActive(true);
        }

        if (alatSekarang == "Bibit")
        {
            
            if (stateSaatIni == stateSpotPohon.TidakTanam)
            {
                pohonGhaib.SetActive(true);
            }
            ActiondalamAreaPohon?.Invoke(this);
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
            Vector3 posisiApi = GetPosisiApiDiAtasPohon();
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
        if (diLokasi && bolehupgrade)
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
            if (diLokasi && stateSaatIni == stateSpotPohon.Tanam && bolehupgrade)
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
        pohonAsli.transform.localScale = Vector3.zero;
        Vector3 targetScale = originalScale * (ukuranRandom / 2f);
        pohonAsli.LeanScale(targetScale, 2f)
            .setEase(LeanTweenType.easeInOutBack);
        spotPohonManager.GantiStatePohon(gameObject.name, "Tanam");
        darahPohon.setMaxDarah(Mydarah);
    }

    public void Tumbuh()
    {
        LeanTween.cancel(pohonAsli.gameObject);
        pohonAsli.transform.localScale = Vector3.zero;
        Vector3 targetScale = originalScale * ukuranRandom;
        pohonAsli.LeanScale(targetScale, 2f)
            .setEase(LeanTweenType.easeInOutBack);
        stateSaatIni = stateSpotPohon.Tumbuh;
        spotPohonManager.GantiStatePohon(gameObject.name, "Tumbuh");
        Mydarah *= 2;
        darahPohon.setMaxDarah(Mydarah);
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
