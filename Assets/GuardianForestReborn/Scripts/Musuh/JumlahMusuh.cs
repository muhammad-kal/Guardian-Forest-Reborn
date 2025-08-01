using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumlahMusuh : MonoBehaviour
{
    public TextMeshProUGUI jumlahText;

    private int jumlah = 5;
    private int max = 5;

    void Start()
    {
        UpdateJumlahText();
    }
    public void SetNilai(int jumlahBaru, int maxBaru)
    {
        jumlah = jumlahBaru;
        max = maxBaru;
        UpdateJumlahText();
    }
    public void UpdateNilai(int Update)
    {
        jumlah += Update;
        UpdateJumlahText();
    }

    private void UpdateJumlahText()
    {
        jumlahText.text = $"{jumlah} / {max}";
    }
}
