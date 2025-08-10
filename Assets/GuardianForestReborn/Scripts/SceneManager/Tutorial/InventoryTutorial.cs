using System.Collections;
using UnityEngine;

public class InventoryTutorial : MonoBehaviour
{
    private string currentItem = null;
    private string lastItemTag = null;
    private Coroutine ambilItemCoroutine = null;

    [SerializeField] private GameObject bibit;
    [SerializeField] private GameObject Gembor;
    private InventoryUI UI;
    public System.Action<string> onItemChanged;
    private Tutorial tutorial;
    private bool OnlyOncePickBibitTutorial = false;
    private bool OnlyOnceDropBibitTutorial = false;
    private bool OnlyOncePickEmberTutorial = false;

    void Start()
    {
        UI = FindObjectOfType<InventoryUI>();
        tutorial = transform.root.GetComponent<Tutorial>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsItemTag(other.tag))
        {
            if (ItemInUse() == null)
            {
                if (!OnlyOncePickBibitTutorial && other.tag == "Bibit")
                {
                    tutorial.NextStep();
                    OnlyOncePickBibitTutorial = true;
                }
                else if (!OnlyOncePickEmberTutorial && other.tag == "Gembor" && tutorial.TriggerCount() == 5)
                {
                    tutorial.NextStep();
                    OnlyOncePickEmberTutorial = true;
                }
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
            if (lastItemTag == other.tag)
                    lastItemTag = null;
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
            if (currentItem == "Bibit" && !OnlyOnceDropBibitTutorial && tutorial.TriggerCount() == 4)
            {
                tutorial.NextStep();
                OnlyOnceDropBibitTutorial = true;
            }
            // else if (currentItem == "Gembor" && !OnlyOnceDropEmberTutorial && tutorial.TriggerCount() == 7)
            // {
            //     tutorial.NextStep();
            //     OnlyOnceDropEmberTutorial = true;
            // }
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
