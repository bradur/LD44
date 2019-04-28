// Date   : 27.04.2019 18:46
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInventoryItem : MonoBehaviour {

    [SerializeField]
    private Text txtName;
    [SerializeField]
    private Text txtKey;
    [SerializeField]
    private Text txtAmmoCount;

    [SerializeField]
    private Image imgIcon;

    [SerializeField]
    private Image imgAmmoIcon;

    [SerializeField]
    private GameObject ammoContainer;

    private InventoryItem inventoryItem;

    private RectTransform rectTransform;

    public void Init(InventoryItem item) {
        inventoryItem = item;
        if (!item.UnlimitedAmmo) {
            SetAmmo(item.Ammo);
            imgAmmoIcon.sprite = item.WeaponConfig.Projectile.PreviewPicture;
        } else {
            txtAmmoCount.text = "";
            imgAmmoIcon.enabled = false;
            ammoContainer.SetActive(false);
        }
        imgIcon.sprite = item.WeaponConfig.PreviewPicture;
        txtName.text = item.WeaponConfig.Name;
        txtKey.text = GetNiceKeycode(item.WeaponConfig.KeyCode);
        rectTransform = GetComponent<RectTransform>();
    }

    public int GetSelectOrder () {
        return inventoryItem.SelectOrder;
    }

    private string GetNiceKeycode(KeyCode keyCode) {
        if (keyCode == KeyCode.Alpha0) {
            return "0";
        }
        if (keyCode == KeyCode.Alpha1) {
            return "1";
        }
        if (keyCode == KeyCode.Alpha2) {
            return "2";
        }
        if (keyCode == KeyCode.Alpha3) {
            return "3";
        }
        if (keyCode == KeyCode.Alpha4) {
            return "4";
        }
        if (keyCode == KeyCode.Alpha5) {
            return "5";
        }
        if (keyCode == KeyCode.Alpha6) {
            return "6";
        }
        if (keyCode == KeyCode.Alpha7) {
            return "7";
        }
        if (keyCode == KeyCode.Alpha8) {
            return "8";
        }
        if (keyCode == KeyCode.Alpha9) {
            return "9";
        }
        return keyCode.ToString();
    }

    public bool Equals(InventoryItem item) {
        return inventoryItem == item;
    }

    public void SetParent(RectTransform rt) {
        rectTransform.SetParent(rt, false);
        /*rectTransform.pivot = new Vector2(0f, 0f);
        rectTransform.anchoredPosition = new Vector2(0, 0);*/
        rectTransform.localPosition = Vector2.zero;
    }
    public void SetAmmo(int ammo) {
        if (!inventoryItem.UnlimitedAmmo) {
            txtAmmoCount.text = ammo.ToString();
        }
    }

}
