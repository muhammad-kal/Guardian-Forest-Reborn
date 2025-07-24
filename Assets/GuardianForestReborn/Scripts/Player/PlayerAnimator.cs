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

    
    private void Start()
    {
        
    }

    public void AnimasiManager(Vector3 pergerakan)
    {
        Vector3 Catcher = pergerakan * 100;
        float normalisasiPergerakan = Mathf.Round(Catcher.magnitude);
        
        if (analog.magnitudePergerakan > 0)
        {
            //animator.transform.forward = pergerakan.normalized;
            
            if (analog.magnitudePergerakan > 60)
            {
                animator.SetFloat("animasiKecepatan", pergerakan.magnitude * kecepatanAnimasi );
                PlayAnimasiLari();
            }
            else if (analog.magnitudePergerakan > 0)
            {
                animator.SetFloat("animasiKecepatan", pergerakan.magnitude * kecepatanAnimasi + .5f);
                PlayAnimasiJalan();

            }

        }
        else
            PlayAnimasiDiem();
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
