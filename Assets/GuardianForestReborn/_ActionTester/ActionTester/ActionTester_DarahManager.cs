using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTester_DarahManager : MonoBehaviour
{
    private void Start()
    {
        ActionTester.myAction += darahBerkurang;
    }

    private void OnDestroy()
    {
        ActionTester.myAction -= darahBerkurang;
    }

    private void darahBerkurang(int damage, int sisaDarah)
    {
        Debug.Log("darahBerkurang dari DARAH MANAGER");
    }
}
