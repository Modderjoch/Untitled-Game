using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInventory : MonoBehaviour
{
    [SerializeField] private Transform hand;
    private Transform itemHolding;

    public bool isHolding = false;

    private string coffeeName;
    private float coffeeAmount;
    private string coffeeType;

    private Vector3 scale;

    public void StoreCoffee()
    {
        //if (itemHolding.gameObject.GetComponent<RawCoffee>())
        //{
        //    RawCoffee rawCoffee = itemHolding.gameObject.GetComponent<RawCoffee>();

        //    coffeeName = rawCoffee.InteractionName;
        //    coffeeAmount = rawCoffee.amount;
        //    coffeeType = rawCoffee.type;

        //    Debug.Log(coffeeName + coffeeAmount + coffeeType);
        //}
        //else if (itemHolding.gameObject.GetComponent<PackedCoffee>())
        //{
        //    PackedCoffee packedCoffee = itemHolding.gameObject.GetComponent<PackedCoffee>();

        //    coffeeName = packedCoffee.InteractionName;
        //    coffeeAmount = packedCoffee.amount;
        //    coffeeType = packedCoffee.type;
        //    Debug.Log(coffeeName + coffeeAmount + coffeeType);
        //}
        //else if (itemHolding.gameObject.GetComponent<CoffeeBeans>())
        //{
        //    CoffeeBeans coffeeBeans = itemHolding.gameObject.GetComponent<CoffeeBeans>();

        //    coffeeName = coffeeBeans._name;
        //    coffeeAmount = coffeeBeans.amount;
        //    coffeeType = coffeeBeans.type;

        //    Debug.Log(coffeeName + coffeeAmount + coffeeType);
        //}
        //else if (itemHolding.gameObject.GetComponent<Bucket>())
        //{
        //    Bucket bucket = itemHolding.gameObject.GetComponent<Bucket>();

        //    coffeeName = bucket._name;
        //    coffeeAmount = bucket.amount;
        //    coffeeType = bucket.type;

        //    Debug.Log(coffeeName + coffeeAmount + coffeeType);
        //}
        //else if (itemHolding.gameObject.GetComponent<CoffeePackage>())
        //{
        //    CoffeePackage coffeePackage = itemHolding.gameObject.GetComponent<CoffeePackage>();

        //    coffeeName = coffeePackage.InteractionName;
        //    coffeeAmount = 0;
        //    coffeeType = coffeePackage.InteractionName;
        //}
        //else
        //{
        //    Debug.Log("Couldn't access the details");
        //    return;
        //}
    }

    public GameObject ReturnObject()
    {
        if(itemHolding != null)
        {
            return itemHolding.gameObject;
        }

        return null;
    }

    public void ResetBool()
    {
        isHolding = false;
    }

    public void HoldItem(GameObject toHoldItem)
    {
        if (isHolding != true)
        {
            scale = toHoldItem.transform.localScale;
            toHoldItem.GetComponent<BoxCollider>().enabled = false;
            toHoldItem.tag = "Untagged";

            itemHolding = toHoldItem.transform;
            itemHolding.SetParent(hand);

            itemHolding.transform.position = hand.position;
            itemHolding.transform.rotation = hand.rotation;

            StoreCoffee();

            isHolding = true;
        }
    }

    public string ReturnName()
    {
        if (isHolding == true && itemHolding != null)
        {
            return coffeeName;
        }
        else
        {
            return null;
        }
    }

    public float ReturnAmount()
    {
        if (isHolding == true && itemHolding != null)
        {
            return coffeeAmount;
        }
        else
        {
            return 0f;
        }
    }

    public string ReturnType()
    {
        if (isHolding == true && itemHolding != null)
        {
            return coffeeType;
        }
        else
        {
            return null;
        }
    }

    public void DropItem(Vector3 newPos, Quaternion newRot, Transform parent, string type)
    {
        if(itemHolding != null && type == coffeeType || itemHolding != null && type == "Normal")
        {
            itemHolding.SetParent(parent, true);

            itemHolding.transform.localScale = scale;

            itemHolding.transform.position = newPos;
            itemHolding.transform.rotation = newRot;

            itemHolding.GetComponent<BoxCollider>().enabled = true;
            itemHolding.tag = "Interactable";

            Debug.Log("Dropped: " + itemHolding.name);

            itemHolding = null;
            isHolding = false;
        }
        else
        {
            if (itemHolding == null)
                Debug.Log("You are currently not holding an item");
            else 
                Debug.Log("This storage space was not meant for this type");
        }
    }
}
