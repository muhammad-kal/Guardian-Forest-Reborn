using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanaman : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform jagungRendererTransform;

    [Header("Settings")]
    [SerializeField] private float kecepatanTumbuh;



    public void TanamanTumbuh()
    {

        //jagungRendererTransform.localScale = Vector3.one * kecepatanTumbuh;
        jagungRendererTransform.gameObject.LeanScale(Vector3.one, 1).setEase(LeanTweenType.easeOutBack);
    }
    public void Terbakar()
    {
        //// Ubah warna menjadi oranye-merah seperti terbakar
        //Renderer renderer = jagungRendererTransform.GetComponentInChildren<Renderer>();
        //if (renderer != null)
        //{
        //    renderer.gameObject.LeanColor(Color.white * 2f, 1);
        //    Debug.Log("b");
        //}
        //// (Opsional) Tambah animasi gemetar seperti terbakar
        //// jagungRendererTransform.gameObject.LeanPunchScale(Vector3.one * 0.2f, 1f, 10, 1f);

        //// (Opsional) Tambah partikel api jika kamu punya prefab-nya
        //Instantiate(apiPrefab, jagungRendererTransform.position, Quaternion.identity, jagungRendererTransform);


        Renderer renderer = jagungRendererTransform.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            // Paksa Unity untuk membuat salinan material agar tidak shared
            Material instanceMat = renderer.material;
            renderer.material = instanceMat;

            // Baru ubah warnanya dengan LeanTween
            renderer.gameObject.LeanColor(Color.black , 1);

            Debug.Log("b");
        }
    }
}
