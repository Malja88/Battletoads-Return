using System.Collections;
using UnityEngine;

public class PatrolingAndAttackingAI : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] public Transform player;
    [SerializeField] public Animator animator;
    [Header("Enemy Walking Points on Scene")]
    [SerializeField] public Transform[] walkingPoint;
    [Header("Attack Variables")]
    [SerializeField] public float walkAfterAttack = 2;
    [SerializeField] public float speed = 0.4f;
    [SerializeField] public float timeToIdle = 2;
    [SerializeField] public float interval;
    [SerializeField] public bool attackPlayer;
    [SerializeField] public bool walkRandom;
    private int waypointIndex = 0;
    private float timer;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        walkRandom = true;
        transform.position = walkingPoint[waypointIndex].transform.position;
    }

    void Update()
    {
        Patroling();
        FlipEnemyTowardsPlayer();
        Attack();
    }
    /// <summary>
    /// Enemy patroling certain space
    /// </summary>
    public void Patroling()
    {
        if (!walkRandom)
            return;
        if (walkRandom)
        {
            if (waypointIndex <= walkingPoint.Length - 1)
            {             
                transform.position = Vector2.MoveTowards(transform.position, walkingPoint[waypointIndex].transform.position, speed * Time.deltaTime);
                if (transform.position == walkingPoint[waypointIndex].transform.position)
                {
                    StartCoroutine(Idle());
                    waypointIndex += 1;
                }
                if (transform.position == walkingPoint[2].position)
                {
                    waypointIndex = 0;
                }
            }
        }
    }
    /// <summary>
    /// Enemy trigger on Player
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("HurtBox"))
        {
            walkRandom= false;
            animator.SetBool("Walk", false);
            attackPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.CompareTag("HurtBox"))
        {
            StartCoroutine(WalkAfter());
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
    }
    /// <summary>
    /// Flips Enemy towards player
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
    IEnumerator Idle()
    {
        animator.SetBool("Walk", false);
        speed = 0;
        yield return new WaitForSeconds(timeToIdle);
        speed = 0.4f;
        animator.SetBool("Walk", true);
    }

    IEnumerator WalkAfter()
    {
        attackPlayer = false;
        walkRandom = false;
        yield return new WaitForSeconds(walkAfterAttack);
        walkRandom = true;
        attackPlayer = false;
        animator.SetBool("Walk", true);
    }
    /// <summary>
    /// Random attacks
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
