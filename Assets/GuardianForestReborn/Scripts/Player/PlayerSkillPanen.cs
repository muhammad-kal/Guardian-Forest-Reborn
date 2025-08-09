using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerAlatSelctor))]
public class Player : MonoBehaviour
{
    [Header("Elements")]
    private PlayerAnimator animatorController;
    private LadangManager ladangManager;
    private PlayerAlatSelctor playerAlatSelector;
    private PlayerController playerController;
    private SpotPohonManager spotPohonManager;
    private SpotPohonManagerTutorial spotPohonManagerTutorial;


    // Start is called before the first frame update
    void Start()
    {

        animatorController = GetComponent<PlayerAnimator>();
        playerAlatSelector = GetComponent<PlayerAlatSelctor>();
        playerController = GetComponent<PlayerController>();
        spotPohonManager = FindObjectOfType<SpotPohonManager>();
        spotPohonManagerTutorial = FindAnyObjectByType<SpotPohonManagerTutorial>();


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
        if (playerController.lokasiSaatIni() == "Ladang")
        {
            if (ladangManager)
                ladangManager.BibitTertanam(posisiBibit);
        }
        else if (playerController.lokasiSaatIni() == "TanamZone")
        {
            if (spotPohonManager)
                spotPohonManager.bibitTertanam(posisiBibit);
            else if (spotPohonManagerTutorial)
                spotPohonManagerTutorial.bibitTertanam(posisiBibit);
        }
    }
    private void SemuaLadangTertanamCallback(LadangManager ladang)
    {
        if (ladang == ladangManager)
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
            playerController.ubahArahMenanam();

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
    public void MenanamPohon(SpotPohon other)
    {
        if (playerController.actionActive && other.SiapDitanam())
        {
            animatorController.PlayMenanam();
            playerController.ubahArahMenanam();
        }
        else
        {
            BerhentiMenanam();
        }
    }
    public void MenanamPohonTutorial(SpotPohonTutorial other)
    {
        if (playerController.actionActive && other.SiapDitanam())
        {
            animatorController.PlayMenanam();
            playerController.ubahArahMenanam();
        }
        else
        {
            BerhentiMenanam();
        }
    }

    private void BerhentiMenanam()
    {
        animatorController.StopMenanam();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ladang")
        {
            animatorController.StopMenanam();
            ladangManager = null;

        }
    }
}
