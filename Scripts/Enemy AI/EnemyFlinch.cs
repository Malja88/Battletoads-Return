using System.Collections;
using UnityEngine;

public class EnemyFlinch : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] public Transform player;
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rb2D;
    [Header("Attack variables")]
    [SerializeField] public bool followPlayer;
    [SerializeField] public float speed;
    [SerializeField] public float attackToPlayerDistance;
    [SerializeField] public float verticalAttackDistance;
    [SerializeField] public float flinchDistance;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        followPlayer = true;
    }
    void Update()
    {
        Follow();
        FlipEnemyTowardsPlayer();
    }
    /// <summary>
    /// Stops enemy walking outside bounds
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Border"))
        {
            rb2D.velocity = Vector2.zero;
            animator.SetBool("Walk", false);
        }
    }
    /// <summary>
    /// Follows player
    /// </summary>
    public void Follow()
    {
        if (!followPlayer)
            return;

        if (Vector2.Distance(transform.position, player.position) > attackToPlayerDistance)
        {
            animator.SetBool("Walk", true);
            Vector2 direction = (player.position - transform.position).normalized;
            rb2D.velocity = new Vector2(direction.x, direction.y) * speed;
        }
        if (Vector2.Distance(transform.position, player.position) <= flinchDistance)
        {
            StartCoroutine(WalkBackToPlayer());
        }
    }
    /// <summary>
    /// Flips enemy towards player
    /// </summary>
    public void FlipEnemyTowardsPlayer()
    {
        Vector3 scale = transform.localScale;
        if (player.position != null)
        {
            if (player.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            else if (player.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
            }
        }
        transform.localScale = scale;
    }
    IEnumerator WalkBackToPlayer()
    {
        followPlayer = false;
        animator.SetBool("Walk", false);
        rb2D.velocity = Vector2.zero;
        RandomAttack(Random.Range(0, 2));
        yield return new WaitForSeconds(2);
        animator.SetBool("Walk", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb2D.velocity = new Vector2(direction.x, direction.y) * -speed;
        yield return new WaitForSeconds(2);
        animator.SetBool("Walk", false);
        rb2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(1.5f);
        followPlayer = true;
    }
    /// <summary>
    /// Choice of random attacks
    /// </summary>
    /// <param name="attack"></param>
    public void RandomAttack(int attack)
    {
        if(attack == 0)
        {
            animator.SetTrigger("Attack");
        }
        if (attack == 1)
        {
            animator.SetTrigger("Attack2");
        }
    }
}
