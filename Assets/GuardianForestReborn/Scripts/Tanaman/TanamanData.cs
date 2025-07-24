using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Tanaman", menuName = "Scriptable Objects/Data Tanaman", order =0)]
public class TanamanData : ScriptableObject
{
    [Header("Settings")]
    public Tanaman tanaman;
}
