// Date   : 29.12.2018 12:52
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewEnemyMoveConfig", menuName = "Custom/New EnemyMoveConfig")]
public class EnemyMoveConfig : ScriptableObject
{
    [Header("General")]

    [SerializeField]
    private Vector2 moveIntervalRange = Vector2.one;
    public Vector2 MoveIntervalRange { get { return moveIntervalRange; } }

    [SerializeField]
    private Vector2 moveDurationRange = Vector2.one;
    public Vector2 MoveDurationRange { get { return moveDurationRange; } }

    [SerializeField]
    private Vector2 moveVelocityRange = Vector2.one;
    public Vector2 MoveVelocityRange { get { return moveVelocityRange; } }

    [SerializeField]
    private float rotationSpeed = 5f;
    public float RotationSpeed { get { return rotationSpeed; } }

    [Header("Following")]

    [SerializeField]
    private float checkInterval = 0.5f;
    public float CheckInterval { get { return checkInterval; } }

    [SerializeField]
    private float playerFollowSpeed = 10f;
    public float PlayerFollowSpeed { get { return playerFollowSpeed; } }

    [SerializeField]
    private float maxFollowRange = 4f;
    public float MaxFollowRange { get { return maxFollowRange; } }

    [SerializeField]
    private float courseCorrectionInterval = 0.2f;
    public float CourseCorrectionInterval { get { return courseCorrectionInterval; } }

}