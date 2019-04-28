// Date   : 28.04.2019 11:29
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMouseCursor : MonoBehaviour
{

    [SerializeField]
    private Sprite cursor;
    [SerializeField]
    private Sprite pointer;
    [SerializeField]
    private Sprite crosshair;

    [SerializeField]
    private Image imgCursor;

    [SerializeField]
    private Canvas parentCanvas;
    public void UseCrosshair()
    {
        imgCursor.sprite = crosshair;
    }
    public void UseCursor() {
        imgCursor.sprite = cursor;
    }
    public void UsePointer() {
        imgCursor.sprite = pointer;
    }

    void Update() {
        Vector2 movePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            Input.mousePosition,
            parentCanvas.worldCamera,
            out movePos
        );

        imgCursor.transform.position = parentCanvas.transform.TransformPoint(movePos);
    }

}
