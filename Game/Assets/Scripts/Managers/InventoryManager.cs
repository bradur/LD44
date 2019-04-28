// Date   : 27.04.2019 17:11
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager main;

    void Awake() {
        main = this;
    }

    private void Start() {
        Init();
    }

    private InventoryConfig config;
    private GameConfig gameConfig;
    public void Init() {
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        config = ConfigManager.main.GetConfig("InventoryConfig") as InventoryConfig;
        config.Init();
        UIShop.main.DisplayItems(config);
    }

    public void ProcessPurchasedItems() {
        foreach(InventoryItem item in config.PurchasedItems) {
            Weapon newWeapon = Instantiate(gameConfig.WeaponPrefab);
            newWeapon.Init(item);
            UIInventoryManager.main.AddItem(item);
        }
    }

    public bool BuyItem(InventoryItem item) {
        if (config.PurchaseItem(item)) {
            UIShop.main.SetCurrency(config.Currency);
            return true;
        }
        return false;
    }

    public bool BuyAmmo(InventoryItem item) {
        if (config.PurchaseAmmo(item)) {
            UIShop.main.SetCurrency(config.Currency);
            return true;
        }
        return false;
    }


    public void UpdateHealth(int newHealth) {
        config.Currency = newHealth;
        UpdateHealth();
    }


    public void UpdateHealth() {
        UIInventoryManager.main.SetHealth(GetHealth());
    }

    public int GetHealth() {
        return config.Currency;
    }

    public InventoryItem GetCurrentItem() {
        return config.GetCurrentItem();
    }

    public void Select(InventoryItem item) {
        config.SelectItem(item);
        UIInventoryManager.main.SelectItem(item);
    }

    public void HideShop() {
        UIShop.main.gameObject.SetActive(false);
    }
    public void ShowShop() {
        UIShop.main.gameObject.SetActive(true);
    }

}
