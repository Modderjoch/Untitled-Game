using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeRoaster : MonoBehaviour, IInteractable
{
    [Header("Machine Data")]
    [SerializeField] public MachineData machineData;

    [Header("Interaction")]
    [SerializeField] private string interactionPrompt;
    [SerializeField] private string interactionPrompt2;
    [SerializeField] private Sprite promptImage;
    [SerializeField] private Sprite promptImage2;
    [SerializeField] private string interactionName;

    public string InteractionPrompt => interactionPrompt;
    public string InteractionPrompt2 => interactionPrompt2;
    public string InteractionName => interactionName;
    public Sprite PromptImage => promptImage;
    public Sprite PromptImage2 => promptImage2;

    public CoffeeData roastedCoffeeData; // Reference to the roasted coffee data.
    public float roastingDuration = 5.0f; // Duration of roasting in seconds.
    public float maxRoastingCapacity = 10.0f; // Maximum amount of coffee the roaster can hold.

    private bool isRoasting = false;
    private float roastingTime = 0.0f;
    private CoffeeBag coffeeBag;
    private float usedWeight;

    [SerializeField] private GameObject roasted;
    [SerializeField] private Transform roastedResult;

    public bool Interact(PlayerInteraction playerInteraction)
    {
        if (isRoasting)
        {
            Debug.Log("Already roasting!");
            return false;
        }

        if (GameObject.Find("Player").GetComponent<HandInventory>().HandsAreEmpty() == false)
        {
            coffeeBag = GameObject.Find("Player").GetComponent<HandInventory>().GetComponentInChildren<CoffeeBag>();
            Debug.Log(coffeeBag + "Got ze cofiebag");
        }

        if (coffeeBag != null && coffeeBag.coffeeData.type == CoffeeData.Type.Raw)
        {
            float amountToFill = Mathf.Min(maxRoastingCapacity, coffeeBag.coffeeData.weight);
            usedWeight = amountToFill;
            coffeeBag.RemoveWeight(amountToFill); // Remove coffee from bag.
            isRoasting = true;

            roastedCoffeeData = coffeeBag.coffeeData;

            // Start roasting process.
            Debug.Log("Started roasting process with " + amountToFill + " kg of coffee..." + roastedCoffeeData.coffeeName + roastedCoffeeData.weight);
            return true;
        }

        return false;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        if (isRoasting)
        {
            StartCoroutine(RoastCoffee(roastingDuration));
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator RoastCoffee(float delaySeconds)
    {
        Debug.Log("Roasting started. Waiting for " + delaySeconds + " seconds...");
        yield return new WaitForSeconds(delaySeconds);

        // Finish roasting.
        Debug.Log("Coffee roasted!");
        isRoasting = false;
        roastingTime = 0.0f;

        GameObject result = Instantiate(roasted, roastedResult);
        result.AddComponent<RoastedCoffee>();

        RoastedCoffee resultComp = result.GetComponent<RoastedCoffee>();
        Debug.Log(resultComp);
        resultComp.interactionName = "Roasted " + roastedCoffeeData.coffeeName;
        resultComp.interactionPrompt2 = "";
        resultComp.interactionPrompt = "to collect";
        resultComp.promptImage = promptImage;

        resultComp.coffeeData = roastedCoffeeData;
        resultComp.coffeeData.weight = usedWeight * machineData.weightLoss;
        resultComp.coffeeData.type = CoffeeData.Type.Roasted;        
    }
}
