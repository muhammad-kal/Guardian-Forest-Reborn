using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent (typeof(PlayerAlatSelctor))]
public class PlayerSkillMenanam : MonoBehaviour
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

        //subscribe BibitCollision
        BibitCollision.bibitOnCollision += BibitCollidedCallback;
        LadangManager.semuaLadangTertanam += SemuaLadangTertanamCallback;
        playerAlatSelector.actionPilihAlat += AlatTerpilihCallback;

    }

    private void OnDestroy()
    {
        BibitCollision.bibitOnCollision -= BibitCollidedCallback;
        LadangManager.semuaLadangTertanam -= SemuaLadangTertanamCallback;
        playerAlatSelector.actionPilihAlat -= AlatTerpilihCallback;

    }

    private void AlatTerpilihCallback(PlayerAlatSelctor.Alat alatTerpilih)
    {
        if (!playerAlatSelector.PilihBibit())
            BerhentiMenanam(); 

    }

    private void BibitCollidedCallback(Vector3[] posisiBibit)
    {
        if (!ladangManager)
            return;

        ladangManager.BibitTertanam(posisiBibit);
    }
    private void SemuaLadangTertanamCallback(LadangManager ladang)
    {
        if(ladang == ladangManager)
        {
            animatorController.StopMenanam();
        }
    }

    public void Menanam(LadangManager other)
    {

        if (other.tag == "Ladang" && other.GetComponent<LadangManager>().isLadangKosong() && playerAlatSelector.PilihBibit() && playerController.actionActive)
        {
            animatorController.PlayMenanam();
            ladangManager = other.GetComponent<LadangManager>();
            //disini bisa munculin tombol diatas
        }
        else
        {
            BerhentiMenanam();
            if (!other.GetComponent<LadangManager>().isLadangPenuh())
                playerController.actionActive = false;
        }
        /*
         Bisa gini juga
        if (other.CompareTag(..string
         */
    }

    private void BerhentiMenanam()
    {
        animatorController.StopMenanam();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Ladang")
        {
            animatorController.StopMenanam();
            ladangManager = null;

        }
    }
}
