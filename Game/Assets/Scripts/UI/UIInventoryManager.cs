// Date   : 27.04.2019 18:50
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIInventoryManager : MonoBehaviour {

    public static UIInventoryManager main;

    void Awake() {
        main = this;
    }
    GameConfig config;

    [SerializeField]
    private RectTransform currentItemParent;

    [SerializeField]
    private RectTransform inventoryItemParent;

    private List<UIInventoryItem> items = new List<UIInventoryItem>();

    private void Start() {
        config = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
    }

    public void AddItem(InventoryItem item) {
        UIInventoryItem uiItem = Instantiate(config.UIInventoryItemPrefab);
        uiItem.Init(item);
        items.Add(uiItem);
        uiItem.SetParent(inventoryItemParent);
    }

    public void ClearItems() {
        foreach(UIInventoryItem uiItem in items) {
            Destroy(uiItem);
        }
        items.Clear();
    }

    public void SelectItem(InventoryItem item) {
        foreach(UIInventoryItem uiItem in items) {
            if (uiItem.Equals(item)) {
                uiItem.SetParent(currentItemParent);
            }
        }
    }

    public void UpdateAmmo(InventoryItem item) {
        foreach(UIInventoryItem uiItem in items) {
            if (uiItem.Equals(item)) {
                uiItem.SetAmmo(item.Ammo);
            }
        }
    }

}
