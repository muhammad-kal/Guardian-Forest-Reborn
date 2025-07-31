using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPohonManager : MonoBehaviour
{
    [SerializeField] List<SpotPohon> semuaPohon;
    private List<EntityState> DataStateSemuaPohon;
    Tutorial tutorial = null;
    private bool TutorialHanyaSekali = false;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            semuaPohon.Add(GetComponentsInChildren<SpotPohon>()[i]);
        }
    }
    public void SetData(List<EntityState> data)
    {
        DataStateSemuaPohon = data;
        GantiStateSemuaPohonStart();
    }

    private void GantiStateSemuaPohonStart()
    {
        foreach (SpotPohon pohon in semuaPohon)
        {
            string nama = pohon.gameObject.name;
            EntityState data = DataStateSemuaPohon.Find(d => d.nama == nama);

            if (data != null)
            {
                switch (data.state)
                {
                    case "Tanam":
                        pohon.startTanam();
                        break;
                    case "Tumbuh":
                        pohon.startTumbuh();
                        break;
                    case "Tidak Tanam":
                        break;
                    default:
                        Debug.LogWarning($"{nama} memiliki state tidak dikenal: {data.state}");
                        break;
                }
            }
        }
    }

    public void GantiStatePohon(string nama, string stateBaru)
    {
        EntityState data = DataStateSemuaPohon.Find(d => d.nama == nama);
        Debug.Log(nama + stateBaru);
        if (data != null)
            data.state = stateBaru;
        else
            Debug.LogWarning($"Nama {nama} tidak ditemukan di DataStateSemuaPohon.");
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
    public void KeperluanLevel(float FireSpawn)
    {
        foreach (var pohon in semuaPohon)
        {
            pohon.WaktuSebelumTerbakar(FireSpawn);
        }
    }
}
