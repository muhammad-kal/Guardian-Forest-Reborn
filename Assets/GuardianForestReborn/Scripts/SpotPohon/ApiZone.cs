using System.Collections;
using UnityEngine;

public class ApiTrigger : MonoBehaviour
{
    private Coroutine timerBakar;
    private SpotPohon spotPohon;

    private void Start()
    {
        spotPohon = GetComponentInParent<SpotPohon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidApi(other))
        {
            MulaiPembakaran();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsValidApi(other))
        {
            MulaiPembakaran();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsValidApi(other))
        {
            HentikanPembakaran();
        }
    }

    private bool IsValidApi(Collider other)
    {
        return other.CompareTag("Api") && spotPohon != null && !spotPohon.sudahDeteksiApi && spotPohon.stateBisaTerbakar();
    }

    private void MulaiPembakaran()
    {
        if (!spotPohon.sudahDeteksiApi && timerBakar == null)
        {
            spotPohon.sudahDeteksiApi = true;
            timerBakar = StartCoroutine(TungguDanBakar());
        }
    }

    private void HentikanPembakaran()
    {
        if (timerBakar != null)
        {
            StopCoroutine(timerBakar);
            timerBakar = null;
            spotPohon.sudahDeteksiApi = false;
        }
    }

    private IEnumerator TungguDanBakar()
    {
        float waktuTunggu = Random.Range(1f, 4f);
        yield return new WaitForSeconds(waktuTunggu);

        spotPohon?.Terbakar();
        timerBakar = null;
    }
}
