using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTester : MonoBehaviour
{
    public static Action<int, int> myAction;

    private void Start()
    {

        myAction += DebugAngka;
        myAction += DebugAngka;
        myAction += DebugString;
        //myAction += DebugString;
        
        myAction?.Invoke(5,10);

    }


    private void DebugAngka(int angka, int angka2)
    {
        Debug.Log(angka + angka2);

    }

    private void DebugString(int angka, int angka2)
    {
        Debug.Log("Hello WOrld");
    }
}
