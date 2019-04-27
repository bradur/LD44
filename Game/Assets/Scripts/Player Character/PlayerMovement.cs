// Date   : 27.04.2019 08:10
// Project: New Unity Project
// Author : bradur

using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody rb;

    Vector2 velocity;

    [SerializeField]
    Animator animator;

    void Start() {
        Init();
    }

    GameConfig config;
    PlayerPosition playerPosition;
    void Init() {
        config = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        playerPosition = ConfigManager.main.GetConfig("PlayerPosition") as PlayerPosition;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            velocity.y = config.PlayerSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            velocity.y = -config.PlayerSpeed;
        }
        else
        {
            velocity.y = 0;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x = config.PlayerSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x = -config.PlayerSpeed;
        }
        else
        {
            velocity.x = 0;
        }
        animator.SetBool("Walking", velocity != Vector2.zero);
    }

    private void FixedUpdate()
    {
        rb.velocity = (-transform.up * velocity.y) + (transform.right * velocity.x);
    }

    private void LateUpdate() {
        playerPosition.playerPosition = transform.position;
    }
}
