using TMPro;
using UnityEngine;

public class RawCoffee : MonoBehaviour, IInteractable
{
    [Header("UI Prompts")]
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;

    [Header("Details")]
    public string _name;
    public float amount;
    public string type;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI labelTitle;
    [SerializeField] private TextMeshProUGUI labelWeight;

    [Header("Misc")]
    [SerializeField] HandInventory handInventory;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    private void Awake()
    {
        labelTitle.text = _name;
        labelWeight.text = amount.ToString() + " kg";
    }

    public bool Outline(PlayerInteraction playerInteraction)
    {
        GetComponentInChildren<Outline>().gameObject.SetActive(true);

        return true;
    }

    public void UpdateAmount(float toUpdateAmount)
    {
        amount = toUpdateAmount;
        labelWeight.text = amount.ToString() + " kg";

        Debug.Log("Amount now is: " + amount);

        if(amount <= 0)
        {
            Destroy(gameObject);
            handInventory.ResetBool();
        }
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        handInventory.HoldItem(gameObject);
        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return true;
    }
}
