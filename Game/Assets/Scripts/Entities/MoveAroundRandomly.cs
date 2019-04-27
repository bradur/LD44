// Date   : 27.04.2019 10:33
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class MoveAroundRandomly : MonoBehaviour
{

    [SerializeField]
    Vector2 moveIntervalRange = Vector2.one;

    [SerializeField]
    Vector2 moveDurationRange = Vector2.one;

    [SerializeField]
    Vector2 moveVelocityRange = Vector2.one;

    [SerializeField]
    private float rotationSpeed = 5f;
    private float timer = 0f;
    private float interval;

    private float moveTimer = 0f;
    private float moveDuration;

    private float moveSpeed = 1f;

    private float minAngle = 0.01f;
    private Vector2 moveDirection = Vector2.up;

    private bool isMoving = false;

    private bool isRotating = false;

    private Vector2 velocity = Vector2.zero;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody rb;

    private Quaternion targetRotation;

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
        interval = Random.Range(moveIntervalRange.x, moveIntervalRange.y);
        timer = 0f;
    }

    private void StartMovement()
    {
        moveDuration = Random.Range(moveDurationRange.x, moveDurationRange.y);
        moveSpeed = Random.Range(moveVelocityRange.x, moveVelocityRange.y);
        moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        velocity = moveDirection * moveSpeed;
        //float angle = Vector2.Angle(transform.position, (Vector2)transform.position + moveDirection);
        float angle = Mathf.Atan2(velocity.y, velocity.x) * 180 / Mathf.PI;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        isRotating = true;
        moveTimer = 0f;
        isMoving = true;
        animator.SetBool("Walking", true);
    }

    private void StopMovement()
    {
        velocity = Vector2.zero;
        rb.velocity = velocity;
        isMoving = false;
        animator.SetBool("Walking", false);
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

    void LateUpdate() {
        if (isRotating) {
            RotateSmoothly();
        }
    }
}
