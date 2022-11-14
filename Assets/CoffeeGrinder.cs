using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoffeeGrinder : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionName => _name;

    public bool Interact(PlayerInteraction playerInteraction)
    {
        return true;
    }
}
