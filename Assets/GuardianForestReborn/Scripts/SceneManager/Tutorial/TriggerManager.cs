using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public void Trigger()
    {
        GetComponentInParent<Tutorial>().NextStep();
    }
}
