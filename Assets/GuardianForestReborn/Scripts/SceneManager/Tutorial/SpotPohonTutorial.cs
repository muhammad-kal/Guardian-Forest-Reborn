using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Mathematics;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class SpotPohonTutorial : MonoBehaviour
{
    [Header("Action")]
    public static Action<SpotPohonTutorial> ActiondalamAreaPohon;

    [Header("Elements")]
    [SerializeField] private GameObject pohonAsli;
    [SerializeField] private GameObject pohonGhaib;
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
    private SpotPohonManagerTutorial spotPohonManagerTutorial;
    Tutorial tutorial = null;
    private Vector3 originalScale;
    private float Mydarah;
    private void Start()
    {
        spotPohonManagerTutorial = GetComponentInParent<SpotPohonManagerTutorial>();
        // Inisialisasi nilai awal (berlaku untuk semua scene)
        tinggiCustom = UnityEngine.Random.Range(2, 5);
        ukuranRandom = UnityEngine.Random.Range(0.5f, 1f);
        pohonAsli.SetActive(false);
        pohonGhaib.SetActive(false);
        stateSaatIni = stateSpotPohon.TidakTanam;

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = true; // agar tidak jatuh
        tutorial = transform.root.GetComponent<Tutorial>();
        originalScale = pohonAsli.transform.localScale;
        Mydarah = 1f;
        if (gameObject.name == "SpotPohonTutorial (10)")
        {
            Terbakar();
        }
    }

    private void TanamTutorial()
    {
        // Aktifkan pohon ghaib dulu (jika ingin animasi singkat atau efek visual lain)
        pohonGhaib.SetActive(true);

        // Ubah state menjadi Tanam, aktifkan pohon asli
        pohonAsli.SetActive(true);
        stateSaatIni = stateSpotPohon.Tanam;

        // Langsung ubah skala pohon tanpa animasi tumbuh
        pohonAsli.transform.localScale = Vector3.one * ukuranRandom;
        stateSaatIni = stateSpotPohon.Tumbuh;

        // Sembunyikan pohon ghaib karena sudah tumbuh
        pohonGhaib.SetActive(false);
    }


    public void masukLokasiTanam(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        diLokasi = true;
        string alatSekarang = other.GetComponent<PlayerController>().alat;

        // if (stateSaatIni == stateSpotPohon.Tumbuh || stateSaatIni == stateSpotPohon.Tanam)
        // {
        //     darahPohon.gameObject.SetActive(true);
        // }

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
            UnityEngine.Vector3 posisiApi;
            if (gameObject.name == "SpotPohonTutorial (10)")
            {
                posisiApi = GetPosisiApiApiUnggun();
            }
            else
            {
                posisiApi = GetPosisiApiDiAtasPohon();
            }
            Instantiate(apiPrefab, posisiApi, Quaternion.identity, transform);
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
    private Vector3 GetPosisiApiApiUnggun()
    {
        Vector3 posisi = pohonAsli.transform.position;
        posisi.y += 2;
        return posisi;
    }

    public void Tertanam()
    {
        if (diLokasi && gameObject.name != "SpotPohonTutorial (10)")
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
            if (diLokasi && stateSaatIni == stateSpotPohon.Tanam && gameObject.name != "SpotPohonTutorial (10)")
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
        spotPohonManagerTutorial.nextsteptutorial();
    }

    public void Tanam()
    {
        pohonAsli.transform.localScale = Vector3.zero;
        Vector3 targetScale = originalScale * (ukuranRandom / 2f);
        pohonAsli.LeanScale(targetScale, 2f)
            .setEase(LeanTweenType.easeInOutBack);
        spotPohonManagerTutorial.GantiStatePohon(gameObject.name, "Tanam");
        darahPohon.setMaxDarah(Mydarah);
        spotPohonManagerTutorial.Tanam10X();
    }

    public void Tumbuh()
    {
        LeanTween.cancel(pohonAsli.gameObject);
        pohonAsli.transform.localScale = Vector3.zero;
        Vector3 targetScale = originalScale * ukuranRandom;
        pohonAsli.LeanScale(targetScale, 2f)
            .setEase(LeanTweenType.easeInOutBack);
        stateSaatIni = stateSpotPohon.Tumbuh;
        // spotPohonManagerTutorial.GantiStatePohon(gameObject.name, "Tumbuh");
        Mydarah *= 2;
        darahPohon.setMaxDarah(Mydarah);
        spotPohonManagerTutorial.Siram10X();
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
}
