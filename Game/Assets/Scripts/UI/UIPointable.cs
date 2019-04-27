// Date   : 27.04.2019 19:12
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;


public class UIPointable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private System.Action<PointerEventData> onPointerEnterCallback;
    private System.Action<PointerEventData> onPointerExitCallback;
    private System.Action<PointerEventData> onPointerClickCallback;
    public void Initialize(
        System.Action<PointerEventData> onPointerEnterCallback,
        System.Action<PointerEventData> onPointerExitCallback,
        System.Action<PointerEventData> onPointerClickCallback
    )
    {
        this.onPointerEnterCallback = onPointerEnterCallback;
        this.onPointerExitCallback = onPointerExitCallback;
        this.onPointerClickCallback = onPointerClickCallback;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (onPointerEnterCallback != null)
        {
            onPointerEnterCallback(pointerEventData);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (onPointerExitCallback != null)
        {
            onPointerExitCallback(pointerEventData);
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (onPointerClickCallback != null)
        {
            onPointerClickCallback(pointerEventData);
        }
    }

}
