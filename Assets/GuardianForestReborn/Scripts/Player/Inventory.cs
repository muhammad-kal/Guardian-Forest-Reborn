using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Inventory : MonoBehaviour
{
    private string currentItem = null;
    private string lastItemTag = null;
    private Coroutine ambilItemCoroutine = null;

    [SerializeField] private GameObject bibit;
    [SerializeField] private GameObject Gembor;
    private InventoryUI UI;
    public System.Action<string> onItemChanged;

    void Start()
    {
        UI = FindObjectOfType<InventoryUI>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsItemTag(other.tag))
        {
            // Jika sedang tidak pegang item
            if (ItemInUse() == null)
            {
                // Jika item sama dengan sebelumnya, tunggu 1 detik
                if (other.tag == lastItemTag)
                {
                    if (ambilItemCoroutine == null)
                    {
                        ambilItemCoroutine = StartCoroutine(TungguAmbilItem(other));
                    }
                }
                else
                {
                    AmbilItemLangsung(other);
                }

                lastItemTag = other.tag;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsItemTag(other.tag))
        {
            if (ambilItemCoroutine != null)
            {
                StopCoroutine(ambilItemCoroutine);
                ambilItemCoroutine = null;
            }
        }
    }

    private bool IsItemTag(string tag)
    {
        return tag == "Bibit" || tag == "Gembor";
    }

    private IEnumerator TungguAmbilItem(Collider other)
    {
        yield return new WaitForSeconds(2f);

        if (other != null && other.gameObject != null)
        {
            AmbilItemLangsung(other);
        }

        ambilItemCoroutine = null;
    }

    private void AmbilItemLangsung(Collider other)
    {
        ChangeItem(other.tag);
        Destroy(other.gameObject);
    }

    public string ItemInUse()
    {
        return currentItem;
    }

    private void ChangeItem(string tag)
    {
        currentItem = tag;
        UI?.ChangeLogo(currentItem);
        onItemChanged?.Invoke(currentItem);
    }

    public void DropItem()
    {
        if (currentItem != null)
        {
            if (currentItem == "Bibit")
            {
                Instantiate(bibit, transform.position, Quaternion.identity);
            }
            else if (currentItem == "Gembor")
            {

                Instantiate(Gembor, transform.position, Quaternion.identity);
            }

            currentItem = null;
            ChangeItem(currentItem);
        }
    }
}
