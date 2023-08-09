using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeightSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private Slider weightSlider;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private int coffeePrice;
    [SerializeField] CoffeeData coffeeData;

    public int totalPrice;
    public int finalWeight;

    private void Start()
    {
        coffeeData = GetComponentInParent<ShopItem>().coffeeData;
        coffeePrice = coffeeData.pricePerKG;
    }

    public void DisplayWeight()
    {
        weightText.text = string.Format("{0} kg", weightSlider.value);
    }

    public void EditPricePerKilo()
    {
        totalPrice = coffeePrice * Mathf.FloorToInt(weightSlider.value);
        finalWeight = Mathf.FloorToInt(weightSlider.value);

        buyText.text = string.Format("BUY / €{0}", coffeePrice * weightSlider.value);
    }
}
