// Date   : 27.04.2019 20:32
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class UIStartLevelButton : MonoBehaviour {


    [SerializeField]
    private UIPointable uiPointable;

    void Start() {
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

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameManager.main.StartNextLevel();
        InventoryManager.main.HideShop();
    }

}
