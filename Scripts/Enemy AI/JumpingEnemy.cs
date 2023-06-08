using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingEnemy : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Animator animator;
    [SerializeField] public Transform player;
    [Header("Attack variables")]
    [SerializeField] public bool followPlayer;
    [SerializeField] public float attackToPlayerDistance;
    [SerializeField] public float speed;
    [SerializeField] public float flinchDistance;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        followPlayer= true;
    }

    private void FixedUpdate()
    {
        Follow();
    }
    private void Update()
    {
        FlipEnemyTowardsPlayer();
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
            rb.velocity = new Vector2(direction.x, direction.y) * speed;
        }
        if (Vector2.Distance(transform.position, player.position) <= flinchDistance)
        {
            StartCoroutine(WalkBackToPlayer());
        }
    }
    /// <summary>
    /// Performs heavy attack on player
    /// </summary>
    public void JumpAttack()
    {
      StartCoroutine(JumpKick());
    }
    IEnumerator JumpKick()
    {
        animator.SetTrigger("Jump");
        rb.gravityScale = 0.637f;
        if (player.position.x < transform.position.x)
        {
            rb.AddForce(new Vector2(-0.5f, 1) * 2, ForceMode2D.Impulse);
        }
        if (player.position.x > transform.position.x)
        {
            rb.AddForce(new Vector2(0.5f, 1) * 2, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = 0;
    }
    /// <summary>
    /// Returns back to plyaer after certain amount of time
    /// </summary>
    /// <returns></returns>
    IEnumerator WalkBackToPlayer()
    {
        followPlayer = false;
        animator.SetBool("Walk", false);
        rb.velocity = Vector2.zero;
        RandomAttack(Random.Range(0, 2));
        yield return new WaitForSeconds(2);
        animator.SetBool("Walk", true);
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * -speed;
        yield return new WaitForSeconds(2);
        animator.SetBool("Walk", false);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(1f);
        followPlayer = true;
    }
    /// <summary>
    /// Random attacks
    /// </summary>
    /// <param name="attack"></param>
    public void RandomAttack(int attack)
    {
        if (attack == 0)
        {
            animator.SetTrigger("Attack");
        }
        if (attack == 1)
        {
            JumpAttack();
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
}
