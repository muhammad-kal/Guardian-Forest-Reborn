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

    public void DalamAreaSpotPohon(SpotPohon pohon)
    {
        
    }
}
