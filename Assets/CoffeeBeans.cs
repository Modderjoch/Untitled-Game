using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeBeans : MonoBehaviour, IInteractable
{
    [Header("UI Prompts")]
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;

    [Header("Details")]
    public string _name;
    public float amount;
    public string type;

    [Header("Misc")]
    public HandInventory handInventory;
    public CoffeeRoaster coffeeRoaster;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

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
