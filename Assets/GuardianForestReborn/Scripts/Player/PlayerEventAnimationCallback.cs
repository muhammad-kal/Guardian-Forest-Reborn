using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventAnimationCallback : MonoBehaviour
{
    [SerializeField] public ParticleSystem partikelBibit;
    [SerializeField] public ParticleSystem partikelAir;


    private void PlayPartikel()
    {
        partikelBibit.Play();
        
    }

    private void PlayAir()
    {
        partikelAir.Play();

    }
}
