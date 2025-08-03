using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] GameObject itemUIBG;
    [SerializeField] GameObject itemUI;

    [SerializeField] Sprite[] kumpulanGambarItem = new Sprite[0];
    TextMeshProUGUI teks;
    void Start()
    {
        teks = transform.Find("Item/Text").GetComponent<TextMeshProUGUI>();
    }
    public void ChangeLogo(string tag)
    {
        // 0 kosong, 1 Gembor, 2 Bibit
        if (tag == null)
        {
            teks.text = "0";
            itemUIBG.SetActive(false);
        }
        else if (tag == "Gembor")
        {

            AnimasiItem(1);
            teks.text = "1";

        }
        else if (tag == "Bibit")
        {
            AnimasiItem(2);
            teks.text = "2";
        }
    }

    public void AnimasiItem(int index)
    {
        index -= 1;
        itemUIBG.SetActive(true);
        itemUI.transform.localScale = Vector3.zero;
        itemUI.GetComponent<Image>().sprite = kumpulanGambarItem[index];
        //LeanScale(targetScale, 2f).setEase(LeanTweenType.easeInOutBack);
        itemUI.transform.LeanScale(Vector3.one, 0.5f).setEaseOutBack();
        //.setOnStart(() => { itemUI.transform.localScale = Vector3.one; });
    }
}
