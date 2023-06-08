using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthScript player = collision.GetComponent<PlayerHealthScript>();
        if (collision.CompareTag("HurtBox"))
        {
            player.TakeDamage(damage);
        }
    }
}
