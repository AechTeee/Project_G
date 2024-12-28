using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnemyController : MonoBehaviour
{
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask playerLayer;

    [Header("Enemy Stat")]
    [SerializeField] public float speed;
    [SerializeField] private float viewRange;

    [Header("Attack Parameters")]
    [SerializeField] private float damage;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
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
        if (PlayerInSight())
        {
            if (player.position.x > transform.position.x && enemyPatrol.isFacingLeft)
            {
                enemyPatrol.Flip();
            }
            //else
            //{
            //    transform.Translate(Vector2.left * speed * 2 * Time.deltaTime);
            //}
        }
        if (PlayerInRange())
        {
            if (cooldownTimer > attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("attack");

            }
        }
        if (enemyPatrol != null)
        {
            if(!PlayerInRange() && cooldownTimer > attackCooldown)
                enemyPatrol.enabled = true;
            else
                enemyPatrol.enabled = false;
        }
    }

    private bool PlayerInSight()
    {
        Vector3 viewSize = new Vector3(boxCollider.bounds.size.x * viewRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        RaycastHit2D canSee = Physics2D.BoxCast(boxCollider.bounds.center - transform.right * transform.localScale.x, viewSize, 0, Vector2.left, 0, playerLayer);
        if(canSee.collider != null)
            return true;
        return false;
    }

    private bool PlayerInRange()
    {
        Collider2D attackRangeSize = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        
        if(attackRangeSize != null)
        {
            playerHealth = attackRangeSize.transform.GetComponent<Health>();
        }
        
        return attackRangeSize != null;
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
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCollider.bounds.center - transform.right  * transform.localScale.x, new Vector3(boxCollider.bounds.size.x * viewRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
