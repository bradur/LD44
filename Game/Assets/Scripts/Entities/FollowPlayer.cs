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

    void Start()
    {
        playerPosition = ConfigManager.main.GetConfig("PlayerPosition") as PlayerPosition;
    }

    void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer > moveConfig.CheckInterval)
        {
            bool playerIsWithinRange = Vector2.Distance(transform.position, playerPosition.playerPosition) < moveConfig.MaxFollowRange;
            if (!isFollowing && playerIsWithinRange && CanSeePlayer())
            {
                StartFollowing();
            }
            if (isFollowing && !playerIsWithinRange)
            {
                StopFollowing();
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
        enemy.EnableShooting();
        moveAround.Reset();
        moveAround.enabled = false;
        isFollowing = true;
        animator.SetBool("Walking", true);
    }

    private void StopFollowing()
    {
        enemy.DisableShooting();
        animator.SetBool("Walking", false);
        moveAround.enabled = true;
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
        if (isFollowing)
        {
            rb.velocity = velocity;
        }
    }

}
