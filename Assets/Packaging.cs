using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StorageSpace;

public class Packaging : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;
    [SerializeField] private string _name;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    [SerializeField] private GameObject bucketStorage;
    [SerializeField] private GameObject bagStorage;
    private InputManager inputManager;

    private float coffeeAmount;
    private string coffeeType;
    private string coffeeName;
    private int numberOfBags;

    private List<GameObject> bags = new List<GameObject>();
    [SerializeField] private Camera camera;
    private Camera mainCam;

    private void Awake()
    {
        inputManager = GameObject.Find("Player").GetComponent<InputManager>();
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        mainCam = Camera.main;
        mainCam.GetComponent<CameraSwitcher>().SwitchToCamera(camera);

        inputManager.EnableDisableControl("Packaging");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (bucketStorage.transform.childCount != 0)
        {
            Bucket bucket = bucketStorage.transform.GetChild(0).GetComponent<Bucket>();
            coffeeAmount = bucket.amount;
            coffeeType = bucket.subType;
            coffeeName = bucket._name;

            if(bagStorage.transform.childCount != 0)
            {
                numberOfBags = bagStorage.transform.GetChild(1).GetComponent<CoffeePackage>().numberOfBags;
            }

            Debug.Log("Type is: " + coffeeName + "/" + coffeeType + " amount is: " + coffeeAmount + "Number of bags: " + numberOfBags);
        }

        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        return true;
    }
    public void OnExit()
    {
        mainCam.GetComponent<CameraSwitcher>().ReturnToMain();
    }
}
