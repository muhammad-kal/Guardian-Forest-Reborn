using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpotPohon : MonoBehaviour
{
    [Header("Action")]
    public static Action<SpotPohon> ActiondalamAreaPohon;

    [Header("Elements")]
    [SerializeField] private GameObject pohonAsli;
    [SerializeField] private GameObject pohonGhaib;
    [SerializeField] private SpotPohonManager spotPohonManager;

    [Header("Settings")]
    private float ukuranRandom;


    public enum stateSpotPohon { TidakTanam, Tanam,  }
    private stateSpotPohon stateSaatIni;
    public bool diLokasi = false;


    private void Start()
    {
        pohonAsli.SetActive(false);
        pohonGhaib.SetActive(false);
        stateSaatIni = stateSpotPohon.TidakTanam;
        ukuranRandom = UnityEngine.Random.Range(.5f, 4f);
    }

    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && stateSaatIni == stateSpotPohon.TidakTanam)
        {
            diLokasi=true;
            pohonGhaib.SetActive(true);
            ActiondalamAreaPohon?.Invoke(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (diLokasi)
        //{
        //    if (other.gameObject.tag == "Player" && stateSaatIni == stateSpotPohon.TidakTanam)
        //    {
        //        Tertanam();
        //        Tumbuh();
        //    }
        //}
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pohonGhaib.SetActive(false);
        }
        diLokasi = false;
    }

    public void Tertanam()
    {
        if (diLokasi)
        {
            pohonGhaib.SetActive(false);
            pohonAsli.SetActive(true);
            stateSaatIni = stateSpotPohon.Tanam;
            Tumbuh();
        }
    }

    private void Tumbuh()
    {
        pohonAsli.gameObject.LeanScale(Vector3.one * 0.1f, 1f).setEase(LeanTweenType.easeInOutBack).setOnComplete(() 
            => pohonAsli.gameObject.LeanScale(Vector3.one * ukuranRandom, 10f));
        
    }
}
