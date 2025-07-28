using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class api : MonoBehaviour
{
    [SerializeField] private float kecepatan = 1f; // unit per detik

    void Update()
    {
        transform.Translate(Vector3.right * kecepatan * Time.deltaTime);
    }
}
