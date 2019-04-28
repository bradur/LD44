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

    [SerializeField]
    private Image imgAmmoIcon;

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
        if (!item.UnlimitedAmmo) {
            imgAmmoIcon.sprite = item.WeaponConfig.Projectile.PreviewPicture;
            SetAmmo(item.Ammo);
            buyAmmoButton.Init(this, item);
        } else {
            imgAmmoIcon.enabled = false;
            buyAmmoButton.gameObject.SetActive(false);
            txtAmmoCount.text = "";
        }
        buyButton.Init(this, item);
        inventoryItem = item;
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
            if (buyAmmoButton.isActiveAndEnabled) {
                buyAmmoButton.EnableButton();
            }
            buyButton.DisableButton();
        }
    }

    public void BuyAmmo() {
        if (InventoryManager.main.BuyAmmo(inventoryItem)) {
            SetAmmo(inventoryItem.Ammo);
        }
    }



}
