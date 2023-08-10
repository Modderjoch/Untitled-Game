using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoastedCoffee : MonoBehaviour, IInteractable
{
    public CoffeeData coffeeData;

    [Header("Interaction")]
    public string interactionPrompt;
    public string interactionPrompt2;
    public Sprite promptImage;
    public Sprite promptImage2;
    public string interactionName;

    public string InteractionPrompt => interactionPrompt;
    public string InteractionPrompt2 => interactionPrompt2;
    public string InteractionName => interactionName;
    public Sprite PromptImage => promptImage;
    public Sprite PromptImage2 => promptImage2;

    public bool Interact(PlayerInteraction playerInteraction)
    {
        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return false;
    }
}
