using TMPro;
using UnityEngine;

public class RawCoffee : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    public string _name;

    public float amount;
    public string type;

    [SerializeField] private TextMeshProUGUI labelTitle;
    [SerializeField] private TextMeshProUGUI labelWeight;

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
        }
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        handInventory.HoldItem(gameObject);
        return true;
    }
}
