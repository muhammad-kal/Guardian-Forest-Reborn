using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerAlatSelctor : MonoBehaviour
{
    [Header("Elemenets")]
    [SerializeField] private Image[] gambarTool;
    

    [Header("Settings")]
    [SerializeField] private Color warnaSelect;

    [Header("Action")]
    public Action<Alat> actionPilihAlat;
    
    private Color putih = Color.white;



    public enum Alat { Kosong, Bibit, Air, Arit }
    public Alat alatAktif;

    private void Start()
    {
        PilihAlat(0);
    }

    public void PilihAlat(int indexAlat)
    {
        alatAktif = (Alat)indexAlat;
        
        for (int i = 0; i < gambarTool.Length; i++)
        {
            gambarTool[i].color = i == indexAlat ? warnaSelect : putih;
        }

        actionPilihAlat?.Invoke(alatAktif);

    }

    public bool PilihBibit()
    {
        return alatAktif == Alat.Bibit;
    }

    public bool PilihSiram()
    {
        return alatAktif == Alat.Air;
    }

    public bool PilihArit()
    {
        return alatAktif == Alat.Arit;
    }

    public bool PilihKosong()
    {
        return alatAktif == Alat.Kosong;
    }


}
