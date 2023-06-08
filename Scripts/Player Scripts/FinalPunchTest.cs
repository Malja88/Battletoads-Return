using UnityEngine;

public class FinalPunchTest : MonoBehaviour
{
    [SerializeField] public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
        BossHealthScript boss = collision.GetComponent<BossHealthScript>();
        if (collision.CompareTag("Enemy") && enemy.currentHealth > 50)
        {
                anim.SetTrigger("Attack3");
        }
        if (collision.CompareTag("Enemy") && enemy.currentHealth <= 50)
        {
                FinalAttack(Random.Range(0,3));
        }
        if(collision.CompareTag("Boss") && boss.currentHealth > 50)
        {
            anim.SetTrigger("Attack3");
        }
        if (collision.CompareTag("Boss") && boss.currentHealth <= 50)
        {
            FinalAttack(Random.Range(0,3));
        }
    }

    public void FinalAttack(int attack)
    {
        if (attack == 0)
        {
            anim.SetTrigger("FinalAttack1");
        }

        if (attack == 1)
        {
            anim.SetTrigger("FinalAttack2");
        }

        if (attack == 2)
        {
            anim.SetTrigger("FinalAttack3");
        }
    }
}
