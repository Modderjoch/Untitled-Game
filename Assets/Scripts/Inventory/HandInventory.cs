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


    public GameObject ReturnObject()
    {
        if(itemHolding != null)
        {
            return itemHolding.gameObject;
        }

        return null;
    }

    public bool HandsAreEmpty()
    {
        if(itemHolding != null)
        {
            return false;
        }

        return true;
    }

    public void HoldItem(GameObject toHoldItem)
    {
        if (isHolding != true)
        {
            toHoldItem.GetComponent<BoxCollider>().enabled = false;
            toHoldItem.tag = "Untagged";

            itemHolding = toHoldItem.transform;
            itemHolding.SetParent(hand);

            itemHolding.transform.position = hand.position;
            itemHolding.transform.rotation = hand.rotation;

            isHolding = true;
        }
    }

    public void DropItem(Vector3 newPos, Quaternion newRot, Transform parent, string type)
    {
        if(itemHolding != null && type == coffeeType || itemHolding != null && type == "Normal")
        {
            if(parent.childCount == 0)
            {
                itemHolding.SetParent(parent, true);

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
                Debug.Log("Storage is already full");
            }            
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
