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

    [SerializeField]
    private bool nextLevel = false;

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
        UIInventoryManager.main.UsePointer();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        UIInventoryManager.main.UseCursor();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (nextLevel) {
            GameManager.main.StartNextLevel();
        } else {
            GameManager.main.StartCurrentLevel();
        }
    }

}
