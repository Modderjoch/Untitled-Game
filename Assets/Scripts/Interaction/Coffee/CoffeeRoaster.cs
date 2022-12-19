using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CoffeeRoaster : MonoBehaviour, IInteractable
{
    [Header("UI Prompts")]
    [SerializeField] private string _prompt;
    [SerializeField] private string _prompt2;

    [Header("Details")]
    [SerializeField] private string _name;
    [SerializeField] private float capacity;
    [SerializeField] [Range(1, 60)] private int roastTime;
    private float origCapacity;
    private float usedCapacity;

    private string coffeeName;
    private float coffeeAmount;
    private string currentType;
    private GameObject coffeeObject;

    [Header("UI Elements")]
    [SerializeField] private GameObject roastCanvas;
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private Image timerImage;
    [SerializeField] private Image capacityImage;
    [SerializeField] private Image typeImage;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI capacityText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Misc")]
    [SerializeField] HandInventory handInventory;
    [SerializeField] GameObject beansPrefab;
    [SerializeField] Transform beanHolder;
    [SerializeField] InputManager inputManager;

    public string InteractionPrompt => _prompt;
    public string InteractionPrompt2 => _prompt2;
    public string InteractionName => _name;

    private void Awake()
    {
        origCapacity = capacity;
    }

    public bool Interact(PlayerInteraction playerInteraction)
    {
        FillRoaster();

        return true;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        RefreshUI();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        roastCanvas.SetActive(true);
        inputManager.EnableDisableControl("coffeeroaster");

        return true;
    }

    private void RefreshUI()
    {
        headerText.text = "Coffee Roaster (" + origCapacity + " KG)"; 
        capacityText.text = usedCapacity + "/" + origCapacity;
        //typeText.text = coffeeName;

        capacityImage.fillAmount = usedCapacity / origCapacity;
    }

    public void StartRoast()
    {
        StartCoroutine(EndRoast(roastTime));
    }

    IEnumerator EndRoast(float seconds)
    {
        float counter = seconds;

        while(counter > 0)
        {
            yield return new WaitForSeconds(1);
            counter--;
            //timerImage.fillAmount = (seconds - counter)/seconds; //Timer goes clockwise
            timerImage.fillAmount = counter / seconds;
            timerText.text = "Time left: " + counter;
        }

        Debug.Log("Timer ended");
        if (beanHolder.childCount == 0)
        {
            var beanComponent = beansPrefab.GetComponent<CoffeeBeans>();
            beanComponent.type = "Beans";
            beanComponent._name = typeText.text;
            beanComponent.amount = usedCapacity * Random.Range(.80f, .88f);
            beanComponent.handInventory = handInventory;
            beanComponent.coffeeRoaster = this;
            Instantiate(beansPrefab, beanHolder);
            ClearRoaster();
        }
    }

    public void OnExit()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        roastCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        inputManager.EnableDisableControl("main");

    }

    private void FillRoaster()
    {
        if (handInventory.ReturnType() == "Raw")
        {
            coffeeName = handInventory.ReturnName();
            typeText.text = coffeeName;
            coffeeAmount = handInventory.ReturnAmount();

            float correct = 0;

            Debug.Log("We got " + coffeeAmount + " kilos of " + coffeeName);

            if (capacity > 0)
            {
                usedCapacity += coffeeAmount;

                Debug.Log("Amount: " + usedCapacity + " Capacity: " + capacity);

                capacity -= coffeeAmount;
                coffeeAmount -= usedCapacity;

                if (capacity < 0)
                {
                    correct = capacity * -1;

                    capacity += correct;
                    coffeeAmount += correct;
                }

                coffeeObject = handInventory.ReturnObject();
                coffeeObject.GetComponent<RawCoffee>().UpdateAmount(coffeeAmount);
                handInventory.StoreCoffee();

                coffeeAmount = 0;
                coffeeName = null;
                correct = 0;

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
    }

    public void ClearRoaster()
    {
            Debug.Log("ClearRoaster");
            usedCapacity = 0;
            capacity = origCapacity;
            RefreshUI();
    }
}
