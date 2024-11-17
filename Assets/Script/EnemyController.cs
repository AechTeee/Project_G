using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnemyController : MonoBehaviour
{
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private float damage;
    [SerializeField] private float viewRange;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform ViewCheck;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;

    [Header("Attack Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;

    private Animator animator;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInRange())
        {
            if(cooldownTimer > attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("attack");

            }
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInRange();
        }
    }

    private bool PlayerInRange()
    {
        Vector3 viewSize = new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center - transform.right * attackRange * transform.localScale.x * 0.5f, viewSize, 0, Vector2.left, 0, playerLayer);
        
        if(hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        
        return hit.collider != null;
    }

    private void causeDamage()
    {
        if(PlayerInRange())
        {
            playerHealth.TakeDamge(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center - transform.right * attackRange * transform.localScale.x * 0.5f,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
