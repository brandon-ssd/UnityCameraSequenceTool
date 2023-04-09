using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public Rigidbody2D body;
    float horizontal;
    float vertical;
    float moveLimiter = 0.75f;
    public float runSpeed = 5f;
    private bool canMove = true;
    private Vector3 moveDir;
    public Animator animator; // your animations could go here, use this to add animation states if you have animations.

    void Start()
    {
        body = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        moveDir = new Vector3(horizontal, vertical).normalized;

    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}