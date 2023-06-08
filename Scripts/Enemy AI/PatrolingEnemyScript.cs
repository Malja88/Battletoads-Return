using System.Collections;
using UnityEngine;

public class PatrolingEnemyScript : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] public Rigidbody2D rb2D;
    [SerializeField] public Transform player;
    [SerializeField] public Animator animator;
    [Header("Attack Variables")]
    [SerializeField] public float speed = 0.4f;
    [SerializeField] public float timeToIdle = 2;
    [SerializeField] public float interval;
    [SerializeField] public float verticalAttackDistance;
    [SerializeField] public float attackToPlayerDistance;
    [SerializeField] public float distanceToStopPatroling = 0.5f;
    [SerializeField] public int walkingPointEnd;
    [SerializeField] public bool walkRandom;
    [SerializeField] public bool attackPlayer;
    [SerializeField] public bool followPlayer;
    private int waypointIndex = 0;
    private float timer;
    [Header("Enemy Walking Points on Scene")]
    private Transform[] walkingPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        walkRandom = true;
        transform.position = walkingPoint[waypointIndex].transform.position;
    }
    public void SetWalkingPoints(Transform[] points)
    {
        walkingPoint = points;
    }
    void Update()
    {
        Patroling();
        Attack();
        FlipEnemyTowardsPlayer();
    }
    private void FixedUpdate()
    {
        Follow();
    }
    /// <summary>
    /// Patrolig certain space on scene
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
                if (transform.position == walkingPoint[walkingPointEnd].position)
                {
                    waypointIndex = 0;
                }
                if(Vector2.Distance(transform.position,player.position) <= distanceToStopPatroling)
                {
                    walkRandom= false;
                    followPlayer = true;
                }
            }
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

        else if (Mathf.Abs((transform.position.y - player.position.y)) <= verticalAttackDistance)
        {
            rb2D.velocity = Vector2.zero;
            animator.SetBool("Walk", false);
            followPlayer = false;
            attackPlayer = true;
        }
    }
    /// <summary>
    /// Attacks player
    /// </summary>
    public void Attack()
    {
        if (!attackPlayer) return;
        timer += Time.deltaTime;
        if (timer >= interval)
        {           
            animator.SetTrigger("Attack");
            timer -= interval;
        }
        
        if (Vector2.Distance(transform.position, player.position) > attackToPlayerDistance + 0.1f)
        {
            attackPlayer = false;
            followPlayer = true;
        }
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

    IEnumerator Idle()
    {
        animator.SetBool("Walk", false);
        speed = 0;
        yield return new WaitForSeconds(timeToIdle);
        speed = 0.4f;
        animator.SetBool("Walk", true);
    }
}
