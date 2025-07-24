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
}
