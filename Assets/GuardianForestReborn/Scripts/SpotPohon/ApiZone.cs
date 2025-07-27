using System.Collections;
using UnityEngine;

public class ApiTrigger : MonoBehaviour
{
    private bool sudahDeteksiApi = false;
    private Coroutine timerBakar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Api") && !sudahDeteksiApi)
        {
            sudahDeteksiApi = true;
            timerBakar = StartCoroutine(TungguDanBakar());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Api"))
        {
            if (timerBakar != null)
            {
                StopCoroutine(timerBakar);
                timerBakar = null;
                sudahDeteksiApi = false; // reset agar bisa deteksi ulang jika api masuk lagi
            }
        }
    }

    private IEnumerator TungguDanBakar()
    {
        float waktuTunggu = Random.Range(1f, 4f);
        yield return new WaitForSeconds(waktuTunggu);

        // Setelah waktu acak selesai, panggil Terbakar
        GetComponentInParent<SpotPohon>().Terbakar();
    }
}
