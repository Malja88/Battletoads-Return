using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] public GameObject enemy;
    [SerializeField] public Rigidbody2D rb2d;
    [SerializeField] public Animator animator;
    [Header("Enemy Health")]
    [SerializeField] public int maxHealth;
    [HideInInspector] public int currentHealth;
    [Header("Script Dependency")]
    [SerializeField] public HealthBarScript barScript;
    private void Start()
    {
        currentHealth = maxHealth;
        barScript.SetMaxHealth(maxHealth);
        barScript.SetHealth(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        barScript.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public void EnemyDeath()
    {
        rb2d.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Death");
        Destroy(enemy, 1.5f);
    }
}