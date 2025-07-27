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
        Renderer renderer = jagungRendererTransform.GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            Material[] materials = renderer.materials;

            foreach (Material mat in materials)
            {
                if (mat.HasProperty("_Warna_Shader")) // <- INI "_Warna_Shader" tuh nama Property public di Shader-nya
                {
                    Color warnaAwal = mat.GetColor("_Warna_Shader");
                    Color warnaAkhir = Color.black;

                    LeanTween.value(renderer.gameObject, warnaAwal, warnaAkhir, 1f)
                        .setOnUpdate((Color val) => {
                            mat.SetColor("_Warna_Shader", val);
                        });
                }
            }

            Debug.Log("Tanaman terbakar - semua material diubah ke hitam.");
        }
    }
        // (Opsional) Tambah animasi gemetar seperti terbakar
        // jagungRendererTransform.gameObject.LeanPunchScale(Vector3.one * 0.2f, 1f, 10, 1f);

        // (Opsional) Tambah partikel api jika kamu punya prefab-nya
        // Instantiate(apiPrefab, jagungRendererTransform.position, Quaternion.identity, jagungRendererTransform);
    }
    


