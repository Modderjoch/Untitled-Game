using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    //UI
    [SerializeField] private Canvas interactableCanvas;
    [SerializeField] private TMP_Text interactableName;

    [SerializeField] private float rayRange = 4f;

    [SerializeField] private GameObject rightHand;
    private GameObject currentSelectedObject;
    private GameObject currentHitObject;

    private bool handsFull;
    private bool interactableIsHit;

    private void Awake()
    {
        handsFull = false;
    }

    void Update()
    {


        Ray ray = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0))); //Create a ray from the center of the camera

        RaycastHit hit; //Store the hit information from the raycast

        if (Physics.Raycast(ray, out hit, rayRange)) //Check if the ray hits something
        {
            if(hit.collider != null && hit.collider.tag == "Interactable" && handsFull == false) //Check if the hit object is an interactable
            {
                interactableName.text = hit.collider.name;
                interactableCanvas.gameObject.SetActive(true);
                interactableIsHit = true;

                currentHitObject = hit.collider.gameObject;
            }
            else
            {
                interactableCanvas.gameObject.SetActive(false);
                interactableIsHit = false;
            }
        }
        else
        {
            interactableCanvas.gameObject.SetActive(false);
            interactableIsHit = false;
        }
    }

    public void OnInteract()
    {
        Debug.Log("do something");

        if(handsFull == false && interactableIsHit)
        {
            currentSelectedObject = currentHitObject;
            currentHitObject = null;

            currentSelectedObject.GetComponent<Rigidbody>().isKinematic = true;
            currentSelectedObject.transform.position = rightHand.transform.position;
            currentSelectedObject.transform.parent = rightHand.transform;

            handsFull = true;
        }
        else
        {
            Debug.Log("hands are full");
        }
    }

    public void OnDrop()
    {
        if(handsFull == true)
        currentSelectedObject.GetComponent<Rigidbody>().isKinematic = false;
        currentSelectedObject.transform.parent = null;

        currentSelectedObject = null;
        currentHitObject = null;

        handsFull = false;
    }
}
