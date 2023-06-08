using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    [SerializeField]
    [Header("Body Components")]
    public new Rigidbody2D rigidbody2D;
    public Animator animator;
    public Transform player;
    [Header("Audio")]
    public StudioEventEmitter shout;
    [Header("Attack variables")]
    public float regularSpeed;
    public float runSpeed;
    public float attackToPlayerDistance;
    public float attackInterval;
    public float ramAttackDistance;
    private float attackTimer;
    public GameObject ramAttackTrigger;
    [Header("Attack booleans")]
    public bool regularAttack;
    public bool followPlayer;
    void Start()
    {
        followPlayer = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate()
    {
        Follow();
        FlipEnemyTowardsPlayer();  
        RegularAttack();

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
            rigidbody2D.velocity = new Vector2(direction.x, direction.y) * regularSpeed;
        }
        else
        {
            followPlayer = false;
            regularAttack = true;
        }
    }
    /// <summary>
    /// Executes regular attack on player
    /// </summary>
    public void RegularAttack()
    {
        if (!regularAttack)
            return;
        if(regularAttack)
        {
            if (Vector2.Distance(transform.position, player.position) <= attackToPlayerDistance)
            {
                rigidbody2D.velocity = Vector2.zero;
                animator.SetBool("Walk", false);
                attackTimer += Time.deltaTime;
                if(attackTimer>=attackInterval)
                {
                    animator.SetTrigger("Attack");
                    attackTimer -= attackInterval;
                }               
                followPlayer =false;
                regularAttack = true;
            }
            if (Vector2.Distance(transform.position, player.position) >= attackToPlayerDistance)
            {
                animator.SetBool("Walk", true);
                followPlayer = true;
                regularAttack = false;
            }
        }
    }
    /// <summary>
    /// Executes heavy attack on player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RamTrigger"))
        {
            followPlayer = false;
            shout.Play();
            animator.SetBool("Walk", false);
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("Run", true);
            Invoke("RunAndFollowPlayer", 1);
            StartCoroutine(RamAttackTriggersSwitcher());
        }
        if(collision.CompareTag("Border"))
        {
            rigidbody2D.velocity = Vector2.zero;
            followPlayer = true;
            animator.SetBool("Run", false);
        }
        if (Vector2.Distance(transform.position, player.position) <= ramAttackDistance)
        {
            StartCoroutine(RamAttack());
        }
    }
    /// <summary>
    /// Runs towards player
    /// </summary>
    public void RunAndFollowPlayer()
    {
        if (player.position.x < transform.position.x)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigidbody2D.velocity = new Vector2(direction.x, direction.y) * runSpeed;
        }
        if (player.position.x > transform.position.x)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rigidbody2D.velocity = new Vector2(direction.x, direction.y) * runSpeed;
        }
    }
    IEnumerator RamAttack()
    {
        animator.SetTrigger("RamAttack");
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Run", false);
        yield return new WaitForSeconds(1);
        followPlayer = true;
    }
    IEnumerator RamAttackTriggersSwitcher()
    {
        ramAttackTrigger.SetActive(false);
        yield return new WaitForSeconds (5);
        ramAttackTrigger.SetActive(true);
    }
    /// <summary>
    /// Flips towards player
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
