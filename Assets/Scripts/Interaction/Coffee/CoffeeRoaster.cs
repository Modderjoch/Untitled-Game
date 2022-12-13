using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoffeeRoaster : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    [SerializeField] private string _name;

    [SerializeField] private float capacity;
    private float origCapacity;
    private float usedCapacity;

    private string coffeeName;
    private float coffeeAmount;
    private GameObject coffeeObject;

    [SerializeField] HandInventory handInventory;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    private void Awake()
    {
        origCapacity = capacity;
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        if(handInventory.ReturnType() == "Raw")
        {
            coffeeName = handInventory.ReturnName();
            coffeeAmount = handInventory.ReturnAmount();

            float correct = 0;

            Debug.Log("We got " + coffeeAmount + " kilos of " + coffeeName);

            if(capacity > 0)
            {
                usedCapacity += coffeeAmount;

                Debug.Log("Amount: " + usedCapacity + " Capacity: " + capacity);

                capacity -= coffeeAmount;
                coffeeAmount -= usedCapacity;

                if(capacity < 0)
                {
                    correct = capacity * -1;

                    capacity += correct;
                    coffeeAmount += correct;
                }

                coffeeObject = handInventory.ReturnObject();
                coffeeObject.GetComponent<RawCoffee>().UpdateAmount(coffeeAmount);
                handInventory.StoreCoffee();

                coffeeAmount= 0;
                coffeeName= null;
                correct= 0;

                usedCapacity = (capacity - origCapacity) * -1;

                Debug.Log("Original capacity: " + origCapacity + " Capacity now: " + capacity + " Currently in use: " + usedCapacity);
            }
            else
            {
                Debug.Log("No capacity");
            }
        }
        else
        {
            Debug.Log("Not the correct type");
        }

        return true;
    }
}
