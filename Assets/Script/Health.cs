using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool dead;
    public float currentHealth { get; private set; }

    [SerializeField] private float staringHealth;
    [SerializeField] private GameController _gameController;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = staringHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamge(float damge)
    {
        currentHealth = Mathf.Clamp(currentHealth - damge, 0, staringHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("die");
                if (GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;
                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                if(GetComponent<AnemyController>() != null)
                    GetComponent<AnemyController>().enabled = false;
            }
            dead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            //Mat mau voi animation
        }
    }
}
