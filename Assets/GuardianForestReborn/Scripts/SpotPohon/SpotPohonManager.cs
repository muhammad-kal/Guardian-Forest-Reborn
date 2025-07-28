using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotPohonManager : MonoBehaviour
{
    [SerializeField] List<SpotPohon> semuaPohon;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            semuaPohon.Add(GetComponentsInChildren<SpotPohon>()[i]);
        }
    }

    public void Menanam()
    {
        for(int i = 0; i < semuaPohon.Count; i++)
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
    public void DalamAreaSpotPohon(SpotPohon pohon)
    {

    }
}
