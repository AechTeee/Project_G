using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private bool isFacingLeft = true;
    private float distance = 1f;

    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform aroundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private float cooldownTimer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InSight())
        {
            if (player.position.x > transform.position.x && isFacingLeft)
            {
                Flip();
            }
            if (Vector2.Distance(transform.position, player.position) > attackRange)
            {
                animator.SetBool("attack", false);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("attack", true);
            }
        }
        else
        {
            if (!IsGrounded() || IsWall())
            {
                animator.SetBool("run", false);
                idleTimer += Time.deltaTime;
                if (idleTimer > idleDuration)
                {
                    idleTimer = 0;
                    Flip();
                }
            }
            else
            {
                animator.SetBool("run", true);
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }          
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.Raycast(aroundCheck.position, Vector2.down, distance, groundLayer);
    }

    private bool IsWall()
    {
        return Physics2D.OverlapCircle(aroundCheck.position, 0.1f, groundLayer);
    }

    private void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        speed = -speed;
    }

    private bool InSight()
    {
        return false;
    }

    private void OnDisable()
    {
        animator.SetBool("run", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(aroundCheck.position, Vector2.down * distance);
    }
}
