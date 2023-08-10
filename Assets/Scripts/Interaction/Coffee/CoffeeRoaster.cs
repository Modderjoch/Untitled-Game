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


    public bool Interact(PlayerInteraction playerInteraction)
    {
        if (isRoasting)
        {
            Debug.Log("Already roasting!");
            return false;
        }

        if (!GameObject.Find("Player").GetComponent<HandInventory>().HandsAreEmpty())
        {
            CoffeeBag coffeeBag = GameObject.Find("Player").GetComponent<HandInventory>().GetComponentInChildren<CoffeeBag>();
        }

        if (coffeeBag != null && coffeeBag.coffeeData.type == CoffeeData.Type.Raw)
        {
            float amountToFill = Mathf.Min(maxRoastingCapacity, coffeeBag.coffeeData.weight);
            coffeeBag.RemoveWeight(amountToFill); // Remove coffee from bag.
            isRoasting = true;
            coffeeBag.coffeeData.type = CoffeeData.Type.Roasted; // Change coffee type to roasted.

            // Start roasting process.
            Debug.Log("Started roasting process with " + amountToFill + " kg of coffee...");
            return true;
        }

        return false;
    }

    public bool ExtraInteract(PlayerInteraction playerInteraction)
    {
        // Roast the coffee if it's in the roaster.
        if (isRoasting)
        {
            roastingTime += Time.deltaTime;
            if (roastingTime >= roastingDuration)
            {
                // Finish roasting.
                Debug.Log("Coffee roasted!");
                isRoasting = false;
                roastingTime = 0.0f;
            }
        }

        return false;
    }
}
