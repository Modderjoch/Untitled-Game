using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    //UI
    [SerializeField] private Canvas interactableCanvas;
    [SerializeField] private TMP_Text interactableName;
    [SerializeField] private TMP_Text interactablePrompt;
    [SerializeField] private TMP_Text interactableWeight;

    [SerializeField] private float rayRange = 4f;

    private bool interact;

    private void Awake()
    {
        interact = false;
    }

    void Update()
    {
        Ray ray = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0))); //Create a ray from the center of the camera
        RaycastHit hit; //Store the hit information from the raycast

        if (Physics.Raycast(ray, out hit, rayRange)) //Check if the ray hits something
        {
            if(hit.collider != null && hit.collider.tag == "Interactable") //Check if the hit object is an interactable
            {
                var interactable = hit.collider.gameObject.GetComponent<IInteractable>();

                interactableName.text = interactable.InteractionName;
                interactablePrompt.text = interactable.InteractionPrompt;

                interactableCanvas.gameObject.SetActive(true);

                if (interactable != null && interact)
                {
                    interactable.Interact(this);
                    interact = false;
                }
            }
        }
        else
        {
            interactableCanvas.gameObject.SetActive(false);
            interact = false;
        }
    }

    public void OnInteract()
    {
        interact = true;
    }
}
