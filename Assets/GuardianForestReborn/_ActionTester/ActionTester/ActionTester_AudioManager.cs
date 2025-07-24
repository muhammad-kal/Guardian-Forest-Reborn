using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTester_AudioManager : MonoBehaviour
{
    
    void Start()
    {
        ActionTester.myAction += suaraKenaDamage;
    }


    private void OnDestroy()
    {
        ActionTester.myAction -= suaraKenaDamage;
    }

    private void suaraKenaDamage(int angka, int angka2)
    {
        Debug.Log("Suara Kena Damage dari Audio Manager");
    }

    


}
