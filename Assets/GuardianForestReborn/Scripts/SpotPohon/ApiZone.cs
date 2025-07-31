using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApiTrigger : MonoBehaviour
{
    private Coroutine timerBakar;
    private SpotPohon spotPohon;
    private HashSet<Collider> apiAktif = new HashSet<Collider>();
    private Coroutine monitoringCoroutine;
    public float waktuTunggu = 10f;

    private void Start()
    {
        spotPohon = GetComponentInParent<SpotPohon>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsValidApi(other))
        {
            apiAktif.Add(other);
            MulaiPembakaran();

            if (monitoringCoroutine == null)
                monitoringCoroutine = StartCoroutine(MonitorApiDiDalam());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsValidApi(other))
        {
            apiAktif.Add(other);
            MulaiPembakaran();

            if (monitoringCoroutine == null)
                monitoringCoroutine = StartCoroutine(MonitorApiDiDalam());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Api"))
        {
            apiAktif.Remove(other);

            if (apiAktif.Count == 0)
                HentikanPembakaran();
        }
    }

    private bool IsValidApi(Collider other)
    {
        return other.CompareTag("Api") && spotPohon != null && spotPohon.stateBisaTerbakar();
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

        if (monitoringCoroutine != null)
        {
            StopCoroutine(monitoringCoroutine);
            monitoringCoroutine = null;
        }

        apiAktif.Clear();
    }

    private IEnumerator TungguDanBakar()
    {
        yield return new WaitForSeconds(waktuTunggu);

        if (apiAktif.Count > 0)
        {
            spotPohon?.Terbakar();
        }

        timerBakar = null;
    }

    // 🧠 Monitor terus collider yang sudah tidak valid (destroyed)
    private IEnumerator MonitorApiDiDalam()
    {
        while (apiAktif.Count > 0)
        {
            var hilang = new List<Collider>();

            foreach (var api in apiAktif)
            {
                if (api == null || !api.gameObject.activeInHierarchy)
                {
                    hilang.Add(api);
                }
            }

            foreach (var mati in hilang)
            {
                apiAktif.Remove(mati);
            }

            if (apiAktif.Count == 0)
            {
                HentikanPembakaran();
                yield break;
            }

            yield return new WaitForSeconds(0.2f); // Cek setiap 0.2 detik
        }

        monitoringCoroutine = null;
    }
}
