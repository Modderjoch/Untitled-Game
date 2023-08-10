using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeBag : MonoBehaviour, IInteractable
{
    [Header("Coffee Data")]
    [SerializeField] public CoffeeData coffeeData;

    [Header("Interaction")]
    [SerializeField] private string interactionPrompt;
    [SerializeField] private string interactionPrompt2;
    [SerializeField] private Sprite promptImage;
    [SerializeField] private Sprite promptImage2;
    [SerializeField] private string interactionName;

    [Header("Bag Information")]
    [SerializeField] TextMeshProUGUI coffeeName;
    [SerializeField] TextMeshProUGUI coffeeWeight;

    public string InteractionPrompt => interactionPrompt;
    public string InteractionPrompt2 => interactionPrompt2;
    public string InteractionName => interactionName;
    public Sprite PromptImage => promptImage;
    public Sprite PromptImage2 => promptImage2;

    public void Start()
    {
        if(coffeeData != null)
        {
            coffeeName.text = coffeeData.name;
            coffeeWeight.text = string.Format("{0}KG", coffeeData.weight);
        }        
    }

    public void RemoveWeight(float toRemove)
    {
        coffeeData.weight -= toRemove;

        coffeeWeight.text = string.Format("{0}KG", coffeeData.weight);
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        if (coffeeData != null)
        {
            Debug.Log("You interacted with the coffee bag. Coffee Name: " + coffeeData.coffeeName);

            GameObject.Find("Player").GetComponent<HandInventory>().HoldItem(gameObject);
        }

        return true; // Return true to indicate the interaction was successful.
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return false; // Return false to indicate that there are no extra interactions for this object.
    }
}
