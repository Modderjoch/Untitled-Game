using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeShop : MonoBehaviour
{
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Transform shopItemParent;
    [SerializeField] private GameObject bagPrefab;
    [SerializeField] private Transform bagParent;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        PopulateShop();
    }

    private void PopulateShop()
    {
        for(int i = 0; i < gameManager.coffeeData.Count; i++)
        {
            GameObject item = Instantiate(shopItemPrefab, shopItemParent);
            ShopItem shopItem = item.GetComponent<ShopItem>();

            shopItem.name.text = gameManager.coffeeData[i].coffeeName;
            shopItem.description.text = gameManager.coffeeData[i].description;
            shopItem.image.sprite = gameManager.coffeeData[i].image;
            shopItem.price.text = string.Format("€{0}", gameManager.coffeeData[i].pricePerKG.ToString());
            shopItem.buyButton.onClick.AddListener(() => shopItem.Buy());

            shopItem.coffeeData = gameManager.coffeeData[i];
        }
    }

    public void BuyItem(int price, CoffeeData coffeeData)
    {
        if (CanAfford(price))
        {
            GameObject newBag = Instantiate(bagPrefab, bagParent);
            CoffeeBag coffeeBag = newBag.GetComponent<CoffeeBag>();
            coffeeBag.coffeeData = coffeeData;
        }        
    }

    private bool CanAfford(int price)
    {
        if (price <= PlayerProgression.Instance.ReturnCurrency())
        {
            PlayerProgression.Instance.RemoveCurrency(price);
            return true;
        }

        return true;
    }
}
