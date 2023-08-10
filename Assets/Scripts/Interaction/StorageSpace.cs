using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StorageSpace : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    [SerializeField] private Sprite promptImage;
    [SerializeField] private Sprite promptImage2;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public Sprite PromptImage => promptImage;
    public Sprite PromptImage2 => promptImage2;

    public string InteractionName => _name;

    public enum storageTypes {Normal, Raw, Packed, Bucket, Bag};
    public storageTypes storageType;

    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material unselectMaterial;
    private MeshRenderer _renderer;
    [SerializeField] HandInventory handInventory;
    [SerializeField] Transform storageSpace;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        handInventory = GameObject.Find("Player").GetComponent<HandInventory>();
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        handInventory.DropItem(storageSpace.position, storageSpace.rotation, storageSpace, storageType.ToString());
        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return true;
    }
}
