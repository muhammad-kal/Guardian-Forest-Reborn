using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Analytics;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Animator animator;
    public KontrollerMobile analog;
    [SerializeField] private ParticleSystem partikel;
    [SerializeField] private ParticleSystem partikelAir;

    [Header("Settings")]
    [SerializeField] private float kecepatanAnimasi;
    
    [SerializeField] float deadzone01 = 0.05f;   // 5% radius: cegah jitter
    [SerializeField] float walkThresh01 = 0.25f; // >25% dianggap jalan
    [SerializeField] float runThresh01  = 0.6f;  // >60% dianggap lari
    [SerializeField] float damp = 0.1f;          // smoothing animator

    
    private void Start()
    {

    }

    public void AnimasiManager(Vector3 pergerakan)
{
    // 0..1 bebas resolusi
    float speed01 = (analog.maxMagnitude > 0f)
        ? Mathf.Clamp01(analog.magnitudePergerakan / analog.maxMagnitude)
        : 0f;

    if (speed01 > deadzone01)
    {
        // opsional: arahkan karakter
        // if (pergerakan.sqrMagnitude > 0.0001f) animator.transform.forward = pergerakan.normalized;

        // Skala kecepatan animasi dari 0..1 → 0..kecepatanAnimasi (bisa dikalikan lagi dengan faktor)
        float animSpeed = speed01 * kecepatanAnimasi;

        if (speed01 >= runThresh01)
        {
            animator.SetFloat("animasiKecepatan", animSpeed, damp, Time.deltaTime);
            PlayAnimasiLari();
        }
        else if (speed01 >= walkThresh01)
        {
            // sedikit offset supaya jalan terasa “hidup”
            animator.SetFloat("animasiKecepatan", animSpeed + 0.5f, damp, Time.deltaTime);
            PlayAnimasiJalan();
        }
        else
        {
            // masih di atas deadzone tapi di bawah walkThresh → merayap
            animator.SetFloat("animasiKecepatan", animSpeed * 0.6f, damp, Time.deltaTime);
            PlayAnimasiJalan();
        }
    }
    else
    {
        animator.SetFloat("animasiKecepatan", 0f, damp, Time.deltaTime);
        PlayAnimasiDiem();
    }
}
    public void AnimasiManagerOtomatis(Vector3 pergerakan)
    {
        //float kecepatanGerak = pergerakan.magnitude * 100;

        //if (kecepatanGerak > 0.1f)
        //{
        //    if (kecepatanGerak >= 20f)
        //    {
        //        animator.SetFloat("animasiKecepatan", pergerakan.magnitude * kecepatanAnimasi);
        //        PlayAnimasiLari();
        //    }
        //    else
        //    {
        //        animator.SetFloat("animasiKecepatan", pergerakan.magnitude * kecepatanAnimasi + 0.5f);
        //        PlayAnimasiJalan();
        //    }
        //}
        //else
        //{
        //    PlayAnimasiDiem();
        //}
        float inputSpeed = Mathf.Clamp01(analog.magnitudePergerakan / 100f);

        if (inputSpeed > 0f)
        {
            animator.SetFloat("animasiKecepatan", inputSpeed * kecepatanAnimasi);

            if (inputSpeed > 0.6f) // threshold lari
            {
                PlayAnimasiLari();
            }
            else
            {
                PlayAnimasiJalan();
            }
        }
        else
        {
            PlayAnimasiDiem();
        }
    }

    private void PlayAnimasiLari()
    {
        animator.Play("Lari");
    }

    private void PlayAnimasiLariGila()
    {
        animator.Play("LariGila");
    }

    private void PlayAnimasiJalan()
    {
        animator.Play("Jalan");
    }

    private void PlayAnimasiDiem()
    {
        animator.Play("Diem");
    }

    public void PlayMenanam()
    {

        animator.SetLayerWeight(1, 1);
    }

    public void StopMenanam()
    {
        animator.SetLayerWeight(1, 0);
    }

    public void StopMenyiram()
    {
        animator.SetLayerWeight(2, 0);
        partikelAir.Stop();

    }

    public void PlayMenyiram()
    {
        animator.SetLayerWeight(2, 1);
        
    }
}
