using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimasiItemDiTanah : MonoBehaviour
{
    [SerializeField] private GameObject item;
    void Start()
    {   
        LeanTween.rotateY(item, 360f, 20f).setLoopPingPong();
        
    }


}
