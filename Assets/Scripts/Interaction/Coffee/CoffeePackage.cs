using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoffeePackage : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    public HandInventory handInventory;
    public int numberOfBags = 5;
    [SerializeField] private GameObject bagModel;
    private BoxCollider boxCollider;

    private void Awake()
    {
        handInventory = GameObject.Find("Player").GetComponent<HandInventory>();

        for(int i = 0; i < numberOfBags; i++)
        {
            Instantiate(bagModel, new Vector3(transform.position.x, transform.position.y + (i * .01f), transform.position.z), Quaternion.identity, transform);
        }

        boxCollider = GetComponent<BoxCollider>();
    }

    private void AdjustHitbox()
    {
        Bounds combinedBounds = new Bounds(transform.position, Vector3.zero);
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer childRenderer in childRenderers)
        {
            combinedBounds.Encapsulate(childRenderer.bounds);
        }

        // Resize the Box Collider to fit the bounds of the children
        boxCollider.center = combinedBounds.center - transform.position;
        boxCollider.size = combinedBounds.size;
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        Debug.Log("Interact!");
        handInventory.HoldItem(gameObject);
        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return true;
    }
}
