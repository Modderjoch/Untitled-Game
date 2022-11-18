using TMPro;
using UnityEngine;

public class RawCoffee : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    public string _name;

    public int amount;
    public string type;

    [SerializeField] private TextMeshProUGUI labelTitle;
    [SerializeField] private TextMeshProUGUI labelWeight;

    [SerializeField] HandInventory handInventory;

    public string InteractionPrompt => _prompt;
    public string InteractionName => _name;

    private void Awake()
    {
        labelTitle.text = _name;
        labelWeight.text = amount.ToString() + " kg";
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        handInventory.HoldItem(gameObject);
        return true;
    }
}
