using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI name;
    public TextMeshProUGUI description;
    public TextMeshProUGUI price;

    public Button buyButton;
    public Slider weightSlider;

    public CoffeeData coffeeData;

    private void Start()
    {
        Debug.Log(coffeeData.name);
    }

    public void Buy()
    {
        int buyPrice = weightSlider.GetComponent<WeightSlider>().totalPrice;
        int finalWeigt = weightSlider.GetComponent<WeightSlider>().finalWeight;

        GetComponentInParent<CoffeeShop>().BuyItem(buyPrice, finalWeigt, coffeeData);
    }
}
