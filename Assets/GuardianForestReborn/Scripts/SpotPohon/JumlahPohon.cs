using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JumlahPohon : MonoBehaviour
{
    public TextMeshProUGUI jumlahText;
    public Image logoImage;

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
    public int SisaPohon()
    {
        return jumlah;
    }

    private void UpdateJumlahText()
    {
        jumlahText.text = $"{jumlah} / {max}";
    }

}
