using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanamZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<SpotPohon>().masukLokasiTanam(other);
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponentInParent<SpotPohon>().keluarLokasiTanam(other);
    }
}
