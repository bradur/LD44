// Date   : 27.04.2019 12:44
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    PlayerPosition playerPosition;

    private float checkTimer = 0f;

    private float courseCorrectionTimer = 0f;

    private bool isFollowing = false;

    [SerializeField]
    private LayerMask rayMask;

    [SerializeField]
    private MoveAroundRandomly moveAround;

    [SerializeField]
    private Enemy enemy;

    private Vector2 direction;

    [SerializeField]
    EnemyMoveConfig moveConfig;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private RotateSmoothlyTowardsDirection rotator;

    [SerializeField]
    private Animator animator;

    private Vector2 velocity;

    private float pushBackTimer = 0f;
    private float pushBackInterval = 0.2f;
    private bool pushBack = false;

    private bool isMoving = false;

    private bool enraged = false;
    private float enragedTimer = 0f;
    private float enragedInterval = 3f;

    void Start()
    {
        playerPosition = ConfigManager.main.GetConfig("PlayerPosition") as PlayerPosition;
    }

    void Update()
    {
        if (enraged) {
            enragedTimer += Time.deltaTime;
            if (enragedTimer > enragedInterval) {
                enragedTimer = 0f;
                enraged = false;
            }
        }
        checkTimer += Time.deltaTime;
        if (pushBack)
        {
            pushBackTimer += Time.deltaTime;
            if (pushBackTimer > pushBackInterval)
            {
                pushBackTimer = 0f;
                pushBack = false;
            }
        }
        if (!pushBack && checkTimer > moveConfig.CheckInterval)
        {
            float distance = Vector2.Distance(transform.position, playerPosition.playerPosition);
            bool playerIsWithinRange = (distance < moveConfig.MaxFollowRange) || enraged;
            bool playerCloseEnough = distance <= moveConfig.CloseEnoughDistance;
            bool playerIsSeen = CanSeePlayer() || enraged;
            if (playerIsSeen && playerIsWithinRange)
            {
                enemy.EnableShooting();
            }
            else
            {
                enemy.DisableShooting();
            }
            if (!isFollowing && playerIsWithinRange && playerIsSeen)
            {
                StartFollowing();
            }
            if (isFollowing && (!enraged && !playerIsWithinRange))
            {
                StopFollowing();
            }
            if (isMoving && isFollowing && playerCloseEnough)
            {
                StopMoving();
            }
            else if (!isMoving && isFollowing)
            {
                StartMoving();
            }
            checkTimer = 0f;
        }

        if (isFollowing)
        {
            courseCorrectionTimer += Time.deltaTime;
            if (courseCorrectionTimer > moveConfig.CourseCorrectionInterval)
            {
                CourseCorrect();
                courseCorrectionTimer = 0f;
            }
        }
    }

    private void StartFollowing()
    {
        moveAround.Reset();
        moveAround.enabled = false;
        isFollowing = true;
        animator.SetBool("Walking", true);
        StartMoving();
    }

    private void StopMoving()
    {
        isMoving = false;
    }

    private void StartMoving()
    {
        isMoving = true;
    }

    public void GetHit()
    {
        enraged = true;
        enragedTimer = 0f;
        CourseCorrect();
        StartFollowing();
    }
    private void StopFollowing()
    {
        animator.SetBool("Walking", false);
        moveAround.enabled = true;
        isFollowing = false;
        StopMoving();
    }

    public void GetPushed()
    {
        pushBack = true;
        isFollowing = false;
    }

    private void CourseCorrect()
    {
        velocity = playerPosition.GetDirection(transform.position) * moveConfig.PlayerFollowSpeed;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * 180 / Mathf.PI;
        rotator.Rotate(Quaternion.AngleAxis(angle, Vector3.forward), moveConfig.RotationSpeed);
    }

    private bool CanSeePlayer()
    {
        Debug.DrawRay(transform.position, playerPosition.GetDirection(transform.position) * moveConfig.MaxFollowRange, Color.green, 10f);
        RaycastHit hit;
        if (Physics.Raycast(
            transform.position,
            playerPosition.GetDirection(transform.position),
            out hit,
            moveConfig.MaxFollowRange,
            rayMask
        ))
        {
            return hit.collider.gameObject.tag == "Player";
        }
        return false;
    }

    private void FixedUpdate()
    {
        if (isFollowing && isMoving)
        {
            rb.velocity = velocity;
        }
    }

}
