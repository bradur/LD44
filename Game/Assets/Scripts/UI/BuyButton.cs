// Date   : 27.04.2019 19:42
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class BuyButton : MonoBehaviour
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

    private bool isDisabled = false;

    public void Init(UIShopItem shopItem, InventoryItem item)
    {
        originalColor = bgImg.color;
        uiShopItem = shopItem;
        if (isDisabled)
        {
            DisableButton();
        }
        txtPrice.text = item.WeaponConfig.BasePrice.ToString();
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

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (uiShopItem != null && !isDisabled)
        {
            uiShopItem.Buy();
        }
    }
}
