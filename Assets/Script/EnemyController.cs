using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnemyController : MonoBehaviour
{
    private float speed = 256f;
    private bool isFacingLeft = true;
    private Animator animator;

    [SerializeField] private Rigidbody2D enemyRB;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRB.velocity = Vector2.left * speed * Time.deltaTime;
        Flip();
        animator.SetFloat("speed", Math.Abs(enemyRB.velocity.x));
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingLeft && IsGrounded() || !isFacingLeft && IsGrounded())
        {
            isFacingLeft = !isFacingLeft;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            speed = -speed;
        }
    }
}
