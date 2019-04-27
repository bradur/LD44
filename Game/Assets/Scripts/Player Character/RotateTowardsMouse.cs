// Date   : 27.04.2019 08:05
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class RotateTowardsMouse : MonoBehaviour {

    float preAngle = 90;

    [SerializeField]
    float minMagnitude = 25f;

    void Start () {
        
    }

    void Update () {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        if (offset.magnitude > minMagnitude) {
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + preAngle);
        }
    }
}
