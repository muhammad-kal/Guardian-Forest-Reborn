using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("ELements")]
    private SpriteRenderer[] semuaSprite;
    private float panjangObjek, posisiAwal;
    public GameObject kamera;

    [Header("Settings")]
    [SerializeField] private float efekParallax;


    private float GetPanjangObjek()
    {
        if (semuaSprite != null) {
            for (int i = 0; i < semuaSprite.Length; i++)
            {
                float panjang = semuaSprite[i].bounds.size.x;
                panjangObjek += panjang;

            }
            return panjangObjek;
        }
        else
        {
            return 0f;
        }
    }

    private void Start()
    {
        semuaSprite = GetComponentsInChildren<SpriteRenderer>();
        posisiAwal = transform.position.x;
        GetPanjangObjek();
        Debug.Log("Panjang Objek" + panjangObjek);
    }

    private void FixedUpdate()
    {
        //float pindahPosisiGambar = (kamera.transform.position.x * (1-efekParallax));
        //float jarakTempuh = (kamera.transform.position.x * efekParallax);

        //transform.position = new Vector3(jarakTempuh + posisiAwal, transform.position.y, transform.position.z);

        //if(pindahPosisiGambar > posisiAwal + panjangObjek) posisiAwal += panjangObjek;
        //else if(pindahPosisiGambar < posisiAwal - panjangObjek) posisiAwal -= panjangObjek;
        Vector3 kameraPos = kamera.transform.position;
        Vector3 kameraForward = kamera.transform.forward;

        // Proyeksi posisi kamera ke arah horizontal kamera (bukan world X biasa)
        float parallaxX = Vector3.Dot(kameraPos, kamera.transform.right); // ini mengganti kamera.position.x biasa

        float pindahPosisiGambar = (parallaxX * (1 - efekParallax));
        float jarakTempuh = (parallaxX * efekParallax);

        transform.position = new Vector3(jarakTempuh + posisiAwal, transform.position.y, transform.position.z);

        if (pindahPosisiGambar > posisiAwal + panjangObjek) posisiAwal += panjangObjek;
        else if (pindahPosisiGambar < posisiAwal - panjangObjek) posisiAwal -= panjangObjek;
    }


}
