using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraksiTanamanShader : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Material[] material;


    private void Update()
    {
        for(int i = 0; i < material.Length; i++)
        {
            material[i].SetVector("_PosisiPlayer_shader ", transform.position);
        }
    }


}
