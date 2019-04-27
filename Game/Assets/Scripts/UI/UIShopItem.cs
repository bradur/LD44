// Date   : 27.04.2019 19:10
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIShopItem : MonoBehaviour {


    [SerializeField]
    private Text txtName;
    [SerializeField]
    private Text txtAmmoCount;

    [SerializeField]
    private Image imgIcon;

    private InventoryItem inventoryItem;

    private RectTransform rectTransform;

    [SerializeField]
    private BuyAmmoButton buyAmmoButton;

    [SerializeField]
    private BuyButton buyButton;

    [SerializeField]
    private GameObject buyButtonContainer;

    public void Init(InventoryItem item) {
        txtName.text = item.WeaponConfig.Name;
        imgIcon.sprite = item.WeaponConfig.PreviewPicture;
        rectTransform = GetComponent<RectTransform>();
        SetAmmo(item.Ammo);
        buyAmmoButton.Init(this, item);
        inventoryItem = item;
        buyButton.Init(this, item);
    }


    public bool Equals(InventoryItem item) {
        return inventoryItem == item;
    }

    public void SetParent(RectTransform rt) {
        rectTransform.SetParent(rt);
    }
    public void SetAmmo(int ammo) {
        txtAmmoCount.text = ammo.ToString();
    }

    public void Buy() {
        if (InventoryManager.main.BuyItem(inventoryItem)) {
            buyButtonContainer.SetActive(false);
            buyAmmoButton.EnableButton();
            buyButton.DisableButton();
        }
    }

    public void BuyAmmo() {
        if (InventoryManager.main.BuyAmmo(inventoryItem)) {
            SetAmmo(inventoryItem.Ammo);
        }
    }



}
