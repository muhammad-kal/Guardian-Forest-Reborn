using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENVI_LevelScript : MonoBehaviour
{
    [Header("Objek Yang Ingin Dianimasikan")]
    [SerializeField] private GameObject[] objekAnimasi;
    [SerializeField] private float floatHeight = 10f;
    [SerializeField] private float floatDuration = 2f;


    private void Start()
    {
        if (objekAnimasi != null)
        {
            for (int i = 0; i < objekAnimasi.Length; i++)
            {
                floatHeight = UnityEngine.Random.Range(3, 10);
                Animasi(i);
            }
        }
    }

    private void Animasi(int i)
    {
        LeanTween.moveLocalY(objekAnimasi[i], objekAnimasi[i].transform.position.y + floatHeight, floatDuration)
            .setEase(LeanTweenType.easeOutBack)
            .setOnComplete(() =>
            {
                LeanTween.moveLocalY(objekAnimasi[i], objekAnimasi[i].transform.position.y - floatHeight, floatDuration)
                    .setEase(LeanTweenType.easeOutBack)
                    .setOnComplete( () => Animasi(i));
            });
    }
}
