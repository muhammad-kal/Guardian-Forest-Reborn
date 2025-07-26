using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Petromak : MonoBehaviour
{
    private Light lampuNgedip;
    [SerializeField, Range(0f, 150f)] private float minimal = 0.5f;
    [SerializeField, Range(0f, 150f)] private float maksimal = 1.2f;
    [SerializeField, Min(0f)] private float durasiNgedip = 0.2f;

    private float timer;

    private void Awake()
    {
        if(lampuNgedip == null) 
            lampuNgedip = GetComponent<Light>();

        PeriksaIntensitasCahaya();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(!(timer >= durasiNgedip))
        {
            return;
        }
        lampuNgedip.intensity = Random.Range(minimal, maksimal);
        timer = 0;
    }
    private void PeriksaIntensitasCahaya()
    {
        if(!(minimal > maksimal))
        {
            return;
        }
        Debug.Log("Ukuran Minimal sudah setara maksimal");
        (minimal, maksimal) = (maksimal, minimal);
    }    



}
