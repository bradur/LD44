// Date   : 27.04.2019 19:06
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIShop : MonoBehaviour
{

    public static UIShop main;

    void Awake() {
        main = this;
    }

    GameConfig config;

    [SerializeField]
    private RectTransform uiShopItemContainer;

    [SerializeField]
    private Text txtCurrency;

    void Start()
    {
        config = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
    }

    public void DisplayItems(InventoryConfig inventoryConfig)
    {
        foreach (InventoryItem item in inventoryConfig.AvailableItems)
        {
            UIShopItem uiShopItem = Instantiate(config.UIShopItemPrefab);
            uiShopItem.Init(item);
            uiShopItem.SetParent(uiShopItemContainer);
        }
        SetCurrency(inventoryConfig.Currency);
    }

    public void SetCurrency(int currency) {
        txtCurrency.text = currency.ToString();
    }

}
