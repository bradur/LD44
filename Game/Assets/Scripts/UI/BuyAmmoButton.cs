// Date   : 27.04.2019 19:42
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class BuyAmmoButton : MonoBehaviour
{


    [SerializeField]
    private UIPointable uiPointable;

    private UIShopItem uiShopItem;

    [SerializeField]
    private Text txtPrice;

    [SerializeField]
    private Image bgImg;


    [SerializeField]
    private Color disabledColor = Color.gray;
    private Color originalColor;
    private bool isDisabled = true;

    private float ammoTimer = 5f;
    private float ammoBuyInterval = 0.2f;

    public void Init(UIShopItem shopItem, InventoryItem item)
    {
        originalColor = bgImg.color;
        if (isDisabled)
        {
            DisableButton();
        }
        txtPrice.text = item.WeaponConfig.Projectile.BasePrice.ToString();
        uiShopItem = shopItem;
        uiPointable = GetComponent<UIPointable>();
        if (uiPointable != null)
        {
            uiPointable.Initialize(
                OnPointerEnter,
                OnPointerExit,
                OnPointerClick
            );
        }
    }


    public void DisableButton()
    {
        isDisabled = true;
        bgImg.color = disabledColor;
    }

    public void EnableButton()
    {
        isDisabled = false;
        bgImg.color = originalColor;
    }

    bool entered = false;
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        entered = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        entered = false;
    }

    void Update() {
        ammoTimer += Time.deltaTime;
        if (ammoTimer > ammoBuyInterval && entered == true && uiShopItem != null && Input.GetMouseButton(0)) {
            uiShopItem.BuyAmmo();
            ammoTimer = 0f;
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        /* if (uiShopItem != null && !isDisabled)
        {
            uiShopItem.BuyAmmo();
        }*/
    }

}
