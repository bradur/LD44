// Date   : 28.04.2019 18:32
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ExitGameButton : MonoBehaviour
{

    UIPointable uiPointable;

    public void Start()
    {
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
        UIInventoryManager.main.UsePointer();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        UIInventoryManager.main.UseCursor();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        GameManager.main.ExitGame();
    }


}
