using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanamZoneKebon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //GetComponentInParent<SpotPohon>().masukLokasiTanam(other);
        //GetComponentInParent<TanahLadang>().masukLokasiKebon(other);
    }

    private void OnTriggerStay(Collider other)
    {
        //GetComponentInParent<SpotPohon>().masukLokasiTanam(other);
    }

    private void OnTriggerExit(Collider other)
    {
        //GetComponentInParent<TanahLadang>().keluarLokasiKebon(other);

    }
}
