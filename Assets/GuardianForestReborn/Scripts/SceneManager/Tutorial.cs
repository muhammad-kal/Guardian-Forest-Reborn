using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject actionButton;
    [SerializeField] private GameObject alat;
    [SerializeField] private GameObject analog;
    [SerializeField] private List<string> kalimat = new List<string>();
    [SerializeField] private List<GameObject> ColliderYangAkanDihapus;
    private int trigger = 0;
    private int ColliderTerhapus = 0;
    PlayerController playerController;

    void Start()
    {
        displayKalimat(-1);
        alat.SetActive(false);
        actionButton.SetActive(false);
        playerController = FindObjectOfType<PlayerController>();
    }
    private void displayKalimat(int k)
    {
        Debug.Log(kalimat[k + 1]);
    }
    public void NextStep()
    {
        switch (trigger)
        {
            case 0:
                //munculin toolbar
                alat.SetActive(true);
                displayKalimat(trigger);
                trigger++;
                break;

            case 1:
                //munculin action button
                actionButton.SetActive(true);
                displayKalimat(trigger);
                trigger++;
                break;

            case 2:
                //suruh nanem
                displayKalimat(trigger);
                trigger++;
                break;

            case 3:
                //suruh siram
                displayKalimat(trigger);
                trigger++;
                break;
            case 4:
                //disuruh jalan lagi
                HapusColliderPertama();
                displayKalimat(trigger);
                trigger++;
                break;
            case 5:
                //ngeliat musuh bakar pohon
                analog.GetComponentInChildren<KontrollerMobile>().HideAnalog();
                analog.GetComponent<EventTrigger>().enabled = false;
                actionButton.SetActive(false);
                displayKalimat(trigger);
                Transform grandChild = transform.Find("GAMEPLAY/MusuhManager");
                if (grandChild != null)
                {
                    grandChild.GetComponent<MusuhManager>().spawnsekali();
                }
                trigger++;
                break;
            case 6:
                //nyamperin musuh karena udah bakar pohon
                Transform target = transform.Find("TargetKejarMusuh");
                playerController.JalanKeTargetX(target);
                displayKalimat(trigger);
                trigger++;
                break;
            case 7:
                //mulai padamin api
                analog.GetComponent<EventTrigger>().enabled = true;
                actionButton.SetActive(true);
                displayKalimat(trigger);
                trigger++;
                break;
            case 8:
                HapusColliderPertama();
                displayKalimat(trigger);
                break;
        }
    }
    public void HapusColliderPertama()
    {
        if (ColliderYangAkanDihapus.Count > 0)
        {
            Debug.Log(ColliderTerhapus);
            GameObject target = ColliderYangAkanDihapus[ColliderTerhapus];
            Collider col = target.GetComponent<Collider>();

            if (col != null)
            {
                ColliderTerhapus++;
                Destroy(col);
            }
        }
    }
}
