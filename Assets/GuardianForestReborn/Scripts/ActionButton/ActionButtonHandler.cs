using UnityEngine;
using UnityEngine.UI;

public class ActionButtonHandler : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button actionButton;

    void Start()
    {
        if (actionButton != null)
        {
            // Tambahkan listener saat tombol ditekan
            actionButton.onClick.AddListener(OnActionButtonPressed);
        }
        else
        {
            Debug.LogWarning("Action Button belum ditetapkan!");
        }
    }

    private void OnActionButtonPressed()
    {
        Debug.Log("Tombol Aksi ditekan!");

        // TODO: Ganti dengan aksi sebenarnya, misalnya:
        PerformAction();
    }

    private void PerformAction()
    {
        // Contoh: lompat
        Debug.Log("Melakukan aksi... (misal lompat)");
        // GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }
}
