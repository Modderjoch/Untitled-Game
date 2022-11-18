using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSpace : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionName => _name;

    [SerializeField] HandInventory handInventory;

    public bool Interact(PlayerInteraction playerInteraction)
    {
        handInventory.DropItem(transform.position, transform.rotation, transform);
        return true;
    }
}
