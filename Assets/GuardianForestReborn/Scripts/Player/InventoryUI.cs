using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
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
        }
        else if (tag == "Gembor")
        {
            teks.text = "1";
        }
        else if (tag == "Bibit")
        {
            teks.text = "2";
        }
    }
}
