using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private GameObject interactableCanvas;
    [SerializeField] private TMP_Text interactableName;
    [SerializeField] private TMP_Text interactablePrompt;
    [SerializeField] private TMP_Text interactablePrompt2;
    [SerializeField] private GameObject promptPanel;

    [Header("Raycast")]
    [SerializeField] private float rayRange = 4f;

    private bool interact;
    private bool extraInteract;
    public bool disableInteract;
    private GameObject lastHit;

    private void Awake()
    {
        interact = false;
    }

    void Update()
    {
        if (disableInteract == false)
        {
            Ray ray = (Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0))); //Create a ray from the center of the camera
            RaycastHit hit; //Store the hit information from the raycast

            if (Physics.Raycast(ray, out hit, rayRange)) //Check if the ray hits something
            {
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Interactable")) //Check if the hit object is an interactable
                {
                    var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
                    lastHit = hit.collider.gameObject;

                    if (hit.transform.gameObject.GetComponentInChildren<Outline>() != null)
                    {
                        hit.transform.gameObject.GetComponentInChildren<Outline>().enabled = true;
                    }

                    interactableName.text = interactable.InteractionName;
                    interactablePrompt.text = interactable.InteractionPrompt;
                    interactablePrompt2.text = interactable.InteractionPrompt2;

                    if (interactablePrompt2.text != "") { promptPanel.SetActive(true); } else { promptPanel.SetActive(false); }

                    interactableCanvas.gameObject.SetActive(true);

                    if (interactable != null && interact)
                    {
                        interactable.Interact(this);
                        interact = false;
                        lastHit = null;
                    }
                    else if (interactable != null && extraInteract)
                    {
                        interactable.ExtraInteract(this);
                        extraInteract = false;
                        lastHit = null;
                    }
                }
            }
            else
            {
                interactableCanvas.gameObject.SetActive(false);
                interactablePrompt2.text = null;
                interact = false;

                if (lastHit != null && lastHit.GetComponentInChildren<Outline>() != null)
                {
                    lastHit.transform.gameObject.GetComponentInChildren<Outline>().enabled = false;
                    lastHit = null;
                }
            }
        }
    }

    public void OnInteract()
    {
        interact = true;
    }

    public void OnExtraInteract()
    {
        extraInteract = true;
    }
}
