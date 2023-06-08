using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    [SerializeField] int damage;  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        BossHealthScript boss = collision.GetComponent<BossHealthScript>();
        if (collision.CompareTag("Enemy"))
        {          
            enemy.TakeDamage(damage);
        }
        if(collision.CompareTag("Boss"))
        {
            boss.TakeDamage(damage);
        }
    }
}
