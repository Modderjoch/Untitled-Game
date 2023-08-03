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

    public CoffeeData coffeeData;

    public void Buy()
    {
        GetComponentInParent<CoffeeShop>().BuyItem(coffeeData.pricePerKG, coffeeData);
    }
}
