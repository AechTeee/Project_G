using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public bool isFacingLeft = true;
    private float distance = 1f;

    [SerializeField] private Transform aroundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private float cooldownTimer;

    private Animator animator;
    private AnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyController = GetComponent<AnemyController>();
    }

    // Update is called once per frame
    void Update()
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
            transform.Translate(Vector2.left * enemyController.speed * Time.deltaTime);
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

    public void Flip()
    {
        isFacingLeft = !isFacingLeft;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        enemyController.speed = -enemyController.speed;
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
