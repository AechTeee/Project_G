using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float cooldownTimer = Mathf.Infinity;

    [SerializeField] private float damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask mobLayer;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;

    private Animator animator;
    private PlayerMovement playerMovement;
    private Health mobHealth;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownTimer > attackCooldown && playerMovement.CanAttack())
        {
            if (Input.GetMouseButton(0))
                MeleeAttack();
            else if (Input.GetMouseButton(1))
            {
                animator.SetTrigger("rangedAttack");
                cooldownTimer = 0;
            }
        }
        cooldownTimer += Time.deltaTime;
    }

    private void MeleeAttack()
    {
        animator.SetTrigger("meleeAttack");
        cooldownTimer = 0;
    }

    private bool AttackRange()
    {
        Vector3 viewSize = new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * 0.5f, viewSize, 0, Vector2.left, 0, mobLayer);

        if (hit.collider != null)
        {
            mobHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void RangedAttack()
    {
        arrows[MultiArrow()].transform.position = firePoint.position;
        arrows[MultiArrow()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int MultiArrow()
    {
        for(int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void causeDamage()
    {
        if (AttackRange())
        {
            mobHealth.TakeDamge(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * 0.4f,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
