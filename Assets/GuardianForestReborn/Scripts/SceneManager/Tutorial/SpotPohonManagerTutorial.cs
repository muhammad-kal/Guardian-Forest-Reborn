using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPohonManagerTutorial : MonoBehaviour
{
    [SerializeField] List<SpotPohonTutorial> semuaPohon;
    Tutorial tutorial = null;
    private JumlahPohon jumlahPohon;
    private int tanam10x = 0;
    private int siram10x = 0;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            semuaPohon.Add(GetComponentsInChildren<SpotPohonTutorial>()[i]);
        }
        jumlahPohon = FindObjectOfType<JumlahPohon>();
        jumlahPohon.SetNilai(3, 3);
        tutorial = transform.root.GetComponent<Tutorial>();
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
    public void nextsteptutorial()
    {
        tutorial?.NextStep();
    }
    public void GantiStatePohon(string name, string state)
    {

    }
    public void Tanam10X()
    {
        tanam10x++;
        if (tanam10x == 10)
        {
            tutorial.NextStep();
        }
    }
    public void Siram10X()
    {
        siram10x++;
        if (siram10x == 10)
        {
            tutorial.NextStep();
        }
    }
}
