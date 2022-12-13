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
        if (itemHolding.gameObject.GetComponent<RawCoffee>())
        {
            RawCoffee rawCoffee = itemHolding.gameObject.GetComponent<RawCoffee>();

            coffeeName = rawCoffee.InteractionName;
            coffeeAmount = rawCoffee.amount;
            coffeeType = rawCoffee.type;

            Debug.Log(coffeeName + coffeeAmount + coffeeType);
        }
        else if (itemHolding.GetComponent<PackedCoffee>().enabled == true)
        {
            PackedCoffee packedCoffee = GetComponent<PackedCoffee>();

            coffeeName = packedCoffee.InteractionName;
            coffeeAmount = packedCoffee.amount;
            coffeeType = packedCoffee.type;
            Debug.Log(coffeeName + coffeeAmount + coffeeType);
        }
        else
        {
            return;
        }
    }

    public GameObject ReturnObject()
    {
        if(itemHolding != null)
        {
            return itemHolding.gameObject;
        }

        return null;
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

    public void DropItem(Vector3 newPos, Quaternion newRot, Transform parent)
    {
        if(itemHolding != null)
        {
            itemHolding.SetParent(parent, true);

            itemHolding.transform.localScale = scale;

            itemHolding.transform.position = newPos;
            itemHolding.transform.rotation = newRot;

            itemHolding.GetComponent<BoxCollider>().enabled = true;
            itemHolding.tag = "Interactable";

            itemHolding = null;
            isHolding = false;
        }
    }
}
