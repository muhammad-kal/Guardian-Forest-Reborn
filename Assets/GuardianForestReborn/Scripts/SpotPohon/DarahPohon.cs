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
    [SerializeField] public float darah = 1f;
    [SerializeField] public float maxDarah = 1f;



    private void Start()
    {
        barDarah = transform.Find("Bar");
        setDarah(darah);
        gameObject.SetActive(false);

    }

    public void setDarah(float nilaiDarah)
    {
        darah = Mathf.Clamp(nilaiDarah, 0f, maxDarah);
        float persen = darah / maxDarah;

        barDarah.localScale = new Vector3(persen, 1f);
        barDarah.GetComponentInChildren<SpriteRenderer>().material.color =
            Color.Lerp(warnaDarahSedikit, warnaDarahBanyak, persen);
    }
    public void setMaxDarah(float nilaiMax)
    {
        maxDarah = nilaiMax;
        setDarah(maxDarah);
    }
    public void kurangiDarah()
    {
        if (darah <= 0)
            GetComponentInParent<SpotPohon>().DarahHabis();
        setDarah(darah - 0.1f * Time.deltaTime);
    }
}
