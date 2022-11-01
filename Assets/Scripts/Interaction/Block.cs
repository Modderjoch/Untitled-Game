using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Block : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionName => _name;

    public bool Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log("Picking up block");
        return true;
    }
}
