using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarahPohon : MonoBehaviour
{
    [Header("Elemets")]
    [SerializeField] Color warnaDarahBanyak;
    [SerializeField] Color warnaDarahSedikit;
    [SerializeField] Transform posisiPohon;
    [SerializeField] Transform barDarah;

    [Header("Settings")]

    [SerializeField] Vector3 offsetPosisi;
    [SerializeField,Range(0,1)] float darah;
    [SerializeField,Range(0,1)] float maxDarah;


    private void Start()
    {
        barDarah = transform.Find("Bar");
        setDarah(darah);
        gameObject.SetActive(false);

    }

    public void setDarah(float darah)
    {
        barDarah.localScale = new Vector3(darah, 1f);
        barDarah.GetComponentInChildren<SpriteRenderer>().material.color = Color.Lerp(warnaDarahSedikit, warnaDarahBanyak, darah);
    }

    private void normalisasiDarah(float darah, float maxDarah)
    {
        
    }

}
