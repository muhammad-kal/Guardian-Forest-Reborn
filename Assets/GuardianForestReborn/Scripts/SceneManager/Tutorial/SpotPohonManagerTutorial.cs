using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPohonManagerTutorial : MonoBehaviour
{
    [SerializeField] List<SpotPohonTutorial> semuaPohon;
    Tutorial tutorial = null;
    private bool TutorialHanyaSekali = false;
    private JumlahPohon jumlahPohon;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            semuaPohon.Add(GetComponentsInChildren<SpotPohonTutorial>()[i]);
        }
        jumlahPohon = FindObjectOfType<JumlahPohon>();
        jumlahPohon.SetNilai(3, 3);
    }

    public void Menanam()
    {
        for (int i = 0; i < semuaPohon.Count; i++)
        {
            if (semuaPohon[i].diLokasi)
            {
                semuaPohon[i].Tertanam();
            }
            else
                continue;
        }
    }
    private SpotPohonTutorial GetPohonTerdekat(Vector3 posisi)
    {
        float radius = 5000f;
        SpotPohonTutorial pohonTerdekat = null;

        foreach (var pohon in semuaPohon)
        {
            float jarak = Vector3.Distance(pohon.transform.position, posisi);
            if (jarak < radius)
            {
                radius = jarak;
                pohonTerdekat = pohon;
            }
        }

        return pohonTerdekat;
    }

    public void AirTersiram(Vector3[] posisiAir)
    {
        foreach (var posisi in posisiAir)
        {
            SpotPohonTutorial pohonTerdekat = GetPohonTerdekat(posisi);
            if (pohonTerdekat == null)
                continue;

            if (!pohonTerdekat.SiapDisiram())
                continue;

            pohonTerdekat.Disiram();
        }
    }

    public void bibitTertanam(Vector3[] posisiBibit)
    {
        foreach (var posisi in posisiBibit)
        {
            SpotPohonTutorial pohonTerdekat = GetPohonTerdekat(posisi);
            if (pohonTerdekat == null)
                continue;

            if (!pohonTerdekat.SiapDitanam())
                continue;

            pohonTerdekat.Tertanam();
        }
    }
    public SpotPohonTutorial GetTargetMusuh(Vector3 dariPosisi)
    {
        SpotPohonTutorial targetTerdekat = null;
        float jarakTerdekat = Mathf.Infinity;

        foreach (var spot in semuaPohon)
        {
            if (spot == null || !spot.IsValidTarget())
            {
                continue;
            }


            float jarak = Vector3.Distance(dariPosisi, spot.transform.position);
            if (jarak < jarakTerdekat)
            {
                jarakTerdekat = jarak;
                targetTerdekat = spot;
            }
        }

        return targetTerdekat;
    }

    public void DalamAreaSpotPohon(SpotPohonTutorial spotPohon)
    {

    }
    public void CekApakahSemuaPohonSudahTidakTerbakarTutorial()
    {
        foreach (var pohon in semuaPohon)
        {
            if (pohon != null && pohon.apakahTerbakar())
            {
                // Ada yang masih terbakar, keluar dari method
                return;
            }
        }

        // Semua pohon tidak terbakar, panggil NextStep() di root
        tutorial = transform.root.GetComponent<Tutorial>();
        if (tutorial != null)
        {
            tutorial.NextStep();
            Debug.Log("annn");
        }
        else
        {
            Debug.LogWarning("Root tidak ditemukan untuk memanggil NextStep()");
        }
    }
    public void nextsteptutorial()
    {
        if (!TutorialHanyaSekali)
        {
            tutorial = transform.root.GetComponent<Tutorial>();
            tutorial.NextStep();
            TutorialHanyaSekali = true;
        } 
    }
}
