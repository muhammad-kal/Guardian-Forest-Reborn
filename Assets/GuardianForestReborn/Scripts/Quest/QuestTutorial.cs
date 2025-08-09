using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestTutorial : MonoBehaviour
{
    /* 
     Paradigma Quest : 
        Scriptable Object - Quest Class
        Quest Objective Class
        Quest Progress Class
     
     */


    [SerializeField] Image logo;
    [SerializeField] TextMeshProUGUI misiText;
    [SerializeField] Slider misiSlider;
    [SerializeField] bool tipeNomor;

    private void Awake()
    {
        misiSlider.gameObject.SetActive(tipeNomor);
    }

    private void Start()
    {
        LeanTween.rotateAround(logo.gameObject, new Vector3 (0,1,0), 360, 2.5f).setLoopClamp();
    }

    


}
