using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAndChasingAI : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] public float attackToPlayerDistance;
    [SerializeField] public float verticalAttackDistance;
    [SerializeField] public float waitTimeTillNextAttack;
    [SerializeField] public float chasingPlayerDistance;
    [SerializeField] public float interval;
    [HideInInspector] public float timer;
    [Header("Chase Player Variables")]
    [SerializeField] public Transform player;
    [HideInInspector] public float currentSpeed;
    [SerializeField] public float maxSpeed;
    [Header("Body Components")]
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D body2D;
    public bool followPlayer, attackPlayer, idle;
    private void Start()
    {
        currentSpeed = maxSpeed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        idle= true;
    }

    private void Update()
    {
        FlipEnemyTowardsPlayer();
        Attack();
        EnemyIdle();
    }

    private void FixedUpdate()
    {
        FollowThePlayer();
    }
    /// <summary>
    /// Method, that allows enemy to follow player
    /// </summary>
    /// 
    public void FollowThePlayer()
    {
        if (!followPlayer)
            return;

        if (Vector2.Distance(transform.position, player.position) > attackToPlayerDistance)
        {
            animator.SetBool("Walk", true);
            Vector2 direction = (player.position - transform.position).normalized;
            body2D.velocity = new Vector2(direction.x, direction.y) * currentSpeed;
        }

        else if (Mathf.Abs((transform.position.y - player.position.y)) <= verticalAttackDistance)
        {
            body2D.velocity = Vector2.zero;
            animator.SetBool("Walk", false);
            followPlayer = false;
            attackPlayer = true;
        }
    }
    /// <summary>
    /// Enemy stops moving in certain distance
    /// </summary>
    public void EnemyIdle()
    {
        if (Vector2.Distance(transform.position, player.position) <= chasingPlayerDistance)
        {
            followPlayer = true;
            idle = false;
        }
    }
    /// <summary>
    /// Attack on player
    /// </summary>
    public void Attack()
    {
        if (!attackPlayer) return;
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            RandomAttack(Random.Range(0, 2));
            timer -= interval;
        }

        if (Vector2.Distance(transform.position, player.position) > attackToPlayerDistance + waitTimeTillNextAttack)
        {
            attackPlayer = false;
            followPlayer = true;
        }
    }
    /// <summary>
    /// Method, that flips the enemy towards player
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
    /// <summary>
    /// Random Attacks
    /// </summary>
    /// <param name="attack"></param>
    public void RandomAttack(int attack)
    {
        if (attack == 0)
        {
            animator.SetTrigger("Attack1");
        }

        if (attack == 1)
        {
            animator.SetTrigger("Attack2");
        }
    }
}
