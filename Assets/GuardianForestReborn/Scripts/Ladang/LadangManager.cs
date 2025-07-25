using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class LadangManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private Transform tanahParent;
    private List<TanahLadang> listTanahLadang = new List<TanahLadang>();


    [Header("Settings")]
    [SerializeField] private TanamanData dataTanaman;
    private LadangState state;
    int jumlahBibitCounter = 0;
    int jumlahLadangTersiram = 0;

    [Header("Actions")]
    public static Action<LadangManager> semuaLadangTertanam;
    public static Action<LadangManager> semuaLadangTersiram;


    private void Start()
    {
        GetSemuaTanah();
        state = LadangState.Kosong;
    }

    [NaughtyAttributes.Button]
    public void SemuaTanahTertanam() // <-- Cheat Semua Tertanam
    {
        for(int i = 0;i <  listTanahLadang.Count;i++)
        {
            TaburBibit(listTanahLadang[i]);
        }
    }

    [NaughtyAttributes.Button]
    public void SemuaTanahTersiram() // <- Cheat Juga
    {
        for(int i = 0; i< listTanahLadang.Count; i++)
        {
            SiramAir(listTanahLadang[i]);
        }
    }

    private void GetSemuaTanah()
    {
        for (int i = 0; i < tanahParent.childCount; i++)
        {
            listTanahLadang.Add(tanahParent.GetChild(i).GetComponent<TanahLadang>());
        }
    }

    public void BibitTertanam(Vector3[] posisiBibit)
    {
        for (int i = 0; i < posisiBibit.Length; i++)
        {
            TanahLadang tanahTerdekat = GetPosisiLadangTerdekat(posisiBibit[i]);

            if(!tanahTerdekat)
                continue;

            if (!tanahTerdekat.isKosong())
                continue;

            TaburBibit(tanahTerdekat);
        }
    }
    public void AirTersiram(Vector3[] posisiAir)
    {
        for (int i = 0; i < posisiAir.Length; i++)
        {
            TanahLadang tanahTerdekat = GetPosisiLadangTerdekat(posisiAir[i]);

            if (!tanahTerdekat)
                continue;
            
            if (!tanahTerdekat.isTertanam())
                continue;
            
            SiramAir(tanahTerdekat);
        }
    }

    private void SiramAir(TanahLadang tanahLadang)
    {
        
        jumlahLadangTersiram++;
        tanahLadang.SiramAir();
        if (jumlahLadangTersiram == listTanahLadang.Count)
            TanahTersiramSemua();
    }


    private void TaburBibit(TanahLadang tanahLadang)
    {
        jumlahBibitCounter++;
        tanahLadang.TaburBibit(dataTanaman);
        if(jumlahBibitCounter >= listTanahLadang.Count)
            TanahPenuhBibit();
    }

    private void TanahTersiramSemua()
    {
        Debug.Log("Tanah Tersiram Semua");
        state = LadangState.Tersiram;
        semuaLadangTersiram?.Invoke(this);
    }

    private void TanahPenuhBibit()
    {
        Debug.Log("Tanah Ladang Penug");
        state = LadangState.Tertanam;
        semuaLadangTertanam?.Invoke(this);
    }

    private TanahLadang GetPosisiLadangTerdekat(Vector3 posisiBibit)
    {
        float radius = 5000;
        int indexListTanahLadang = -1;
        for (int i = 0; i < listTanahLadang.Count; i++)
        {
            TanahLadang tanahSaatIni = listTanahLadang[i];
            float jarakTerdekatKeBibit = Vector3.Distance(tanahSaatIni.transform.position, posisiBibit);

            if (jarakTerdekatKeBibit < radius)
            {
                indexListTanahLadang = i;
                radius = jarakTerdekatKeBibit;
            }
        }
        if (radius == 5000)
            return null;

        return listTanahLadang[indexListTanahLadang];
    }

    public bool isLadangKosong()
    {
        return state == LadangState.Kosong;
    }

    public bool isLadangPenuh()
    {
        return state == LadangState.Tertanam;
    }

    public bool isLadangTersiram()
    {
        return state == LadangState.Tersiram;
    }
}


