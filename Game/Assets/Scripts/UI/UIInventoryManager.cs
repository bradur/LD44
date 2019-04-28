// Date   : 27.04.2019 18:50
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIInventoryManager : MonoBehaviour
{

    public static UIInventoryManager main;

    void Awake()
    {
        main = this;
    }
    GameConfig config;

    [SerializeField]
    private RectTransform currentItemParent;

    [SerializeField]
    private RectTransform inventoryItemParent;

    [SerializeField]
    private Text txtHealth;

    private List<UIInventoryItem> items = new List<UIInventoryItem>();

    private UIInventoryItem selectedItem = null;

    [SerializeField]
    private UIMouseCursor uIMouseCursor;

    public void UseCrosshair()
    {
        uIMouseCursor.UseCrosshair();
    }
    public void UseCursor()
    {
        uIMouseCursor.UseCursor();
    }
    public void UsePointer()
    {
        uIMouseCursor.UsePointer();
    }

    private void Start()
    {
        config = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
    }

    public void SetHealth(int health)
    {
        txtHealth.text = health.ToString();
    }

    public void AddItem(InventoryItem item)
    {
        UIInventoryItem uiItem = Instantiate(config.UIInventoryItemPrefab);
        uiItem.Init(item);
        items.Add(uiItem);
        uiItem.SetParent(inventoryItemParent);
    }

    public void ClearItems()
    {
        foreach (UIInventoryItem uiItem in items)
        {
            Destroy(uiItem.gameObject);
        }
        items.Clear();
    }

    public void SelectUIItem(UIInventoryItem item)
    {
        item.SetParent(currentItemParent);
        selectedItem = item;
    }

    public void SelectItem(InventoryItem item)
    {

        foreach (UIInventoryItem uiItem in items)
        {
            if (uiItem.Equals(item))
            {
                if (selectedItem != null)
                {
                    selectedItem.SetParent(inventoryItemParent);
                }
                SelectUIItem(uiItem);
            }
        }
    }

    public void UpdateAmmo(InventoryItem item)
    {
        foreach (UIInventoryItem uiItem in items)
        {
            if (uiItem.Equals(item))
            {
                uiItem.SetAmmo(item.Ammo);
            }
        }
    }

}
