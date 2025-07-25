using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerAlatSelctor))]
public class PlayerSkillMenyiram : MonoBehaviour
{
    [Header("Elements")]
    private PlayerAnimator animatorController;
    private LadangManager ladangManager;
    private PlayerAlatSelctor playerAlatSelector;
    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        
        animatorController = GetComponent<PlayerAnimator>();
        playerAlatSelector = GetComponent<PlayerAlatSelctor>();
        playerController = GetComponent<PlayerController>();


        //subscribe AirCollsion
        AirCollision.airOnCollision += AirCollidedCallback;
        LadangManager.semuaLadangTertanam += SemuaLadangTersiramCallback; //BELUM GANTI <---
        playerAlatSelector.actionPilihAlat += AlatTerpilihCallback;

    }

    private void OnDestroy()    
    {
        AirCollision.airOnCollision -= AirCollidedCallback;
        LadangManager.semuaLadangTersiram -= SemuaLadangTersiramCallback;
        playerAlatSelector.actionPilihAlat -= AlatTerpilihCallback;

    }

    private void AlatTerpilihCallback(PlayerAlatSelctor.Alat alatTerpilih)
    {
        if (!playerAlatSelector.PilihSiram())
            BerhentiMenyiram(); Debug.Log(alatTerpilih.ToString());

    }

    private void AirCollidedCallback(Vector3[] posisiAir)
    {
        if (!ladangManager)
            return;

        ladangManager.AirTersiram(posisiAir);
    }

    private void SemuaLadangTersiramCallback(LadangManager ladang)
    {
        if (ladang == ladangManager)
        {
            animatorController.StopMenyiram();
        }
    }

    public void Menyiram(LadangManager other)
    {

        if (other.tag == "Ladang" && other.GetComponent<LadangManager>().isLadangPenuh() && playerAlatSelector.PilihSiram() && playerController.actionActive)
        {
            animatorController.PlayMenyiram();
            ladangManager = other.GetComponent<LadangManager>();
            //disini bisa munculin tombol diatas
        }
        else
        {
            BerhentiMenyiram();
            if(!other.GetComponent<LadangManager>().isLadangKosong())
                playerController.actionActive = false;
        }
        /*
         Bisa gini juga
        if (other.CompareTag(..string
         */
    }

    private void BerhentiMenyiram()
    {
        animatorController.StopMenyiram();
    }

    // private void OnTriggerStay(Collider other)
    // {
    //     MemasukiLadang(other.GetComponent<LadangManager>());

    // }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladang")
        {
            animatorController.StopMenyiram();
            ladangManager = null;

        }
    }
}
