using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Bucket : MonoBehaviour, IInteractable
{
    [Header("UI Prompts")]
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;

    [Header("Details")]
    public string _name;
    public float amount;
    public string type;
    [Range(0, 50)] public int capacity;
    public string subType;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI labelTitle;
    [SerializeField] private TextMeshProUGUI labelWeight;

    [Header("Misc")]
    public HandInventory handInventory;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    private void Awake()
    {
        handInventory = GameObject.Find("Player").GetComponent<HandInventory>();

        labelTitle.text = _name;
        labelWeight.text = amount.ToString() + " kg";
    }

    private void RefreshUI()
    {
        labelTitle.text = _name;
        labelWeight.text = amount.ToString() + " kg";
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        handInventory.HoldItem(gameObject);
        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return true;
    }

    public bool FillBucket(float toAddAmount, string coffeeName, string type)
    {
        if(capacity > toAddAmount)
        {
            subType = type;
            amount += Mathf.Round(toAddAmount * 100.0f) * .01f;
            _name = coffeeName;

            RefreshUI();

            return true;
        }
        else
        {
            Debug.Log("No capacity in this bucket left");

            return false;
        }        
    }
}
