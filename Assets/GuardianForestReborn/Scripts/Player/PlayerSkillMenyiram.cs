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
    private SpotPohonManager spotPohonManager;
    private SpotPohonManagerTutorial spotPohonManagerTutorial;
    private PlayerAlatSelctor playerAlatSelector;
    private PlayerController playerController;
    private string stateSaatIni;


    // Start is called before the first frame update
    void Start()
    {

        animatorController = GetComponent<PlayerAnimator>();
        playerAlatSelector = GetComponent<PlayerAlatSelctor>();
        playerController = GetComponent<PlayerController>();
        spotPohonManager = FindObjectOfType<SpotPohonManager>();
        spotPohonManagerTutorial = FindObjectOfType<SpotPohonManagerTutorial>();

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
            BerhentiMenyiram();
    }

    private void AirCollidedCallback(Vector3[] posisiAir)
    {
        if (playerController.lokasiSaatIni() == "TanahLadangZone")
        {
            if (ladangManager)
                ladangManager.AirTersiram(posisiAir);
        }
        else if (playerController.lokasiSaatIni() == "TanamZone")
        {
            if (spotPohonManager)
                spotPohonManager.AirTersiram(posisiAir);
            else if (spotPohonManagerTutorial)
                spotPohonManagerTutorial.AirTersiram(posisiAir);
        }
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

        if (other.tag == "Ladang" && other.GetComponent<LadangManager>().isLadangPenuh() && /*playerAlatSelector.PilihSiram()*/ playerController.alat == "Gembor" && playerController.actionActive)
        {
            animatorController.PlayMenyiram();
            ladangManager = other.GetComponent<LadangManager>();
            playerController.ubahArahMenanam();
            //disini bisa munculin tombol diatas
        }
        else
        {
            BerhentiMenyiram();
            if (!other.GetComponent<LadangManager>().isLadangKosong())
                playerController.actionActive = false;
        }
        /*
         Bisa gini juga
        if (other.CompareTag(..string
         */
    }

    public void MenyiramApi(SpotPohon other)
    {
        if (playerController.actionActive && other.SiapDisiram())
        {
            animatorController.PlayMenyiram();
            playerController.ubahArahMenanam();
        }
        else
        {
            BerhentiMenyiram();
        }
    }
    public void MenyiramApiTutorial(SpotPohonTutorial other)
    {
        if (playerController.actionActive && other.SiapDisiram())
        {
            animatorController.PlayMenyiram();
            playerController.ubahArahMenanam();
        }
        else
        {
            BerhentiMenyiram();
        }
    }
    public void BerhentiMenyiram()
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
