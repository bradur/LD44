using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewPlayerPosition", menuName = "Custom/New PlayerPosition")]
public class PlayerPosition : ScriptableObject
{

    public Vector2 playerPosition;

    public float GetDistance(Vector2 position)
    {
        return Vector2.Distance(playerPosition, position);
    }

    public Vector2 GetDirection(Vector2 position)
    {
        Vector2 heading = playerPosition - position;
        return heading / heading.magnitude;
    }

}