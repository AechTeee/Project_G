using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private bool dead;
    public float currentHealth { get; private set; }

    [SerializeField] private float startingHealth;
    private Animator animator;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamge(float damge)
    {
        currentHealth = Mathf.Clamp(currentHealth - damge, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("die");
                foreach (var component in components)
                    component.enabled = false;
                dead = true;
                FindAnyObjectByType<UIManager>().GameOver();
                Debug.Log("Nhân vật đã chết! Triggering Game Over...");
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(Invunerability());

        foreach (var component in components)
            component.enabled = true;
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
