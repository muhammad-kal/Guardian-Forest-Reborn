using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanamZoneTutorial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<SpotPohonTutorial>().masukLokasiTanam(other);
    }

    private void OnTriggerStay(Collider other)
    {
        GetComponentInParent<SpotPohonTutorial>().masukLokasiTanam(other);
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<SpotPohonTutorial>().keluarLokasiTanam(other);
    }
}
