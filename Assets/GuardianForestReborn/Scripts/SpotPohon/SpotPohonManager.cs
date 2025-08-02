using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPohonManager : MonoBehaviour
{
    [SerializeField] List<SpotPohon> semuaPohon;
    private SaveData DataStateSemuaPohon;
    JumlahPohon UIJumlahPohon;
    private float darah;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            semuaPohon.Add(GetComponentsInChildren<SpotPohon>()[i]);
        }
        UIJumlahPohon = FindObjectOfType<JumlahPohon>();
    }
    public void KeperluanLevel(float FireSpawn, float maxDarah, SaveData data)
    {
        foreach (var pohon in semuaPohon)
        {
            pohon.WaktuSebelumTerbakar(FireSpawn);
        }
        darah = maxDarah;
        SetData(data);
    }
    public void SetData(SaveData data)
    {
        DataStateSemuaPohon = data;
        GantiStateSemuaPohonStart();
    }

    private void GantiStateSemuaPohonStart()
    {
        int pohonTumbuh = 0;
        int JumlahPohon = 0;
        foreach (SpotPohon pohon in semuaPohon)
        {
            string nama = pohon.gameObject.name;
            EntityState data = DataStateSemuaPohon.GetEntity(nama);
            JumlahPohon++;

            if (data != null)
            {
                switch (data.state)
                {
                    case "Tanam":
                        pohonTumbuh++;
                        pohon.SetDarah(darah/2);
                        pohon.startTanam();
                        break;
                    case "Tumbuh":
                        pohonTumbuh++;
                        pohon.SetDarah(darah);
                        pohon.startTumbuh();
                        break;
                    case "Tidak Tanam":
                        pohon.SetDarah(darah/2);
                        break;
                    default:
                        Debug.LogWarning($"{nama} memiliki state tidak dikenal: {data.state}");
                        break;
                }
            }
        }
        UIJumlahPohon.SetNilai(pohonTumbuh, JumlahPohon);
    }

    public void GantiStatePohon(string nama, string stateBaru)
    {
        DataStateSemuaPohon.SetState(nama, stateBaru);
        if (stateBaru == "Tidak Tanam")
            UIJumlahPohon.UpdateNilai(-1);
        else if (stateBaru == "Tanam")
            UIJumlahPohon.UpdateNilai(1);

        if (UIJumlahPohon.SisaPohon() == 0)
            transform.root.GetComponent<Level>().GameSelesai();
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
    private SpotPohon GetPohonTerdekat(Vector3 posisi)
    {
        float radius = 5000f;
        SpotPohon pohonTerdekat = null;

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
            SpotPohon pohonTerdekat = GetPohonTerdekat(posisi);
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
            SpotPohon pohonTerdekat = GetPohonTerdekat(posisi);
            if (pohonTerdekat == null)
                continue;

            if (!pohonTerdekat.SiapDitanam())
                continue;

            pohonTerdekat.Tertanam();
        }
    }
    public SpotPohon GetTargetMusuh(Vector3 dariPosisi)
    {
        SpotPohon targetTerdekat = null;
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

    public void DalamAreaSpotPohon(SpotPohon spotPohon)
    {

    }
}
