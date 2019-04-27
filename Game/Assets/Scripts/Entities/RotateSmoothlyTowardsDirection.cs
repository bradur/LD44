// Date   : 27.04.2019 13:01
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class RotateSmoothlyTowardsDirection : MonoBehaviour
{

    private float minAngle = 0.01f;

    Quaternion targetRotation;

    private bool isRotating = false;
    private float rotationSpeed;

    public void Rotate(Quaternion newRotation, float speed)
    {
        targetRotation = newRotation;
        rotationSpeed = speed;
        isRotating = true;
    }

    void RotateSmoothly()
    {
        //targetRotation = Quaternion.Euler(moveDirection);
        if (Quaternion.Angle(transform.localRotation, targetRotation) > minAngle)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, step);
        }
        else
        {
            transform.localRotation = targetRotation;
            isRotating = false;
        }
    }

    void LateUpdate()
    {
        if (isRotating)
        {
            RotateSmoothly();
        }
    }

}
