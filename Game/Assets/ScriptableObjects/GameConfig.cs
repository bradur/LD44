// Date   : 29.12.2018 12:52
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewGameConfig", menuName = "Custom/New GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("General")]

    [SerializeField]
    private float playerSpeed = 10f;
    public float PlayerSpeed { get { return playerSpeed; } }

}