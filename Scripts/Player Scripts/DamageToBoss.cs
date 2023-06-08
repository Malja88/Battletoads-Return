using UnityEngine;

public class DamageToBoss : MonoBehaviour
{
    [SerializeField] int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BossHealthScript boss = collision.GetComponent<BossHealthScript>();
        if (collision.CompareTag("Boss"))
        {
            boss.TakeDamage(damage);
        }
    }
}
