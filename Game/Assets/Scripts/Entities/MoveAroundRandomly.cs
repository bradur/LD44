// Date   : 27.04.2019 10:33
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class MoveAroundRandomly : MonoBehaviour
{

    [SerializeField]
    EnemyMoveConfig moveConfig;

    private float timer = 0f;
    private float interval;

    private float moveTimer = 0f;
    private float moveDuration;

    private float moveSpeed = 1f;


    private Vector2 moveDirection = Vector2.up;

    private bool isMoving = false;

    private Vector2 velocity = Vector2.zero;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody rb;

    private Quaternion targetRotation;

    [SerializeField]
    private RotateSmoothlyTowardsDirection rotator;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        ResetInterval();
    }

    private void ResetInterval()
    {
        interval = Random.Range(moveConfig.MoveIntervalRange.x, moveConfig.MoveIntervalRange.y);
        timer = 0f;
    }

    private void StartMovement()
    {
        moveDuration = Random.Range(moveConfig.MoveDurationRange.x, moveConfig.MoveDurationRange.y);
        moveSpeed = Random.Range(moveConfig.MoveVelocityRange.x, moveConfig.MoveVelocityRange.y);
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        velocity = moveDirection * moveSpeed;
        //float angle = Vector2.Angle(transform.position, (Vector2)transform.position + moveDirection);
        float angle = Mathf.Atan2(velocity.y, velocity.x) * 180 / Mathf.PI;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rotator.Rotate(targetRotation, moveConfig.RotationSpeed);
        moveTimer = 0f;
        isMoving = true;
        animator.SetBool("Walking", true);
    }

    public void Reset() {
        StopMovement();
        ResetInterval();
    }

    private void StopMovement()
    {
        velocity = Vector2.zero;
        rb.velocity = velocity;
        isMoving = false;
        animator.SetBool("Walking", false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > interval && !isMoving)
        {
            StartMovement();
        }
        if (isMoving)
        {
            moveTimer += Time.deltaTime;
            if (moveTimer > moveDuration)
            {
                StopMovement();
                ResetInterval();
            }
        }
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = velocity;
        }
    }


}
