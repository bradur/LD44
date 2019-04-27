// Date   : 27.04.2019 08:10
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    float speed = 1;

    [SerializeField]
    private Rigidbody rb;

    Vector2 velocity;

    [SerializeField]
    Animator animator;

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x = speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x = -speed;
        }
        else
        {
            velocity.x = 0;
        }
        animator.SetBool("Walking", velocity != Vector2.zero);
    }

    private void FixedUpdate()
    {
        rb.velocity = (-transform.up * velocity.y) + (Vector3.right * velocity.x);
    }
}
