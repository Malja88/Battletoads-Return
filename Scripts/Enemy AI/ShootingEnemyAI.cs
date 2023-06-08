using System.Collections;
using UnityEngine;

public class ShootingEnemyAI : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] public Rigidbody2D body;
    [SerializeField] public Transform player;
    [SerializeField] public GameObject flames;
    [SerializeField] public Transform firePoint;
    [SerializeField] public Animator animator;
    [Header("Attack Variables")]
    [SerializeField] public bool shoot;
    [SerializeField] public bool walkRandom;
    [SerializeField] public float speed = 0.4f;
    [SerializeField] public float distanceToStopPatroling = 0.5f;
    [SerializeField] public float timeToIdle = 2;
    private int waypointIndex = 0;
    private float timer;
    private float interval = 3;
    [Header("Enemy Walking Points on Scene")]
    private Transform[] walkingPoint;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        walkRandom = true;
    }

    private void Update()
    {
        FlipEnemyTowardsPlayer();
        Patroling();
        Shooting();
    }
    /// <summary>
    /// Sets walking points on scene after spawning
    /// </summary>
    /// <param name="points"></param>
    public void SetWalkingPoints(Transform[] points)
    {
        walkingPoint = points;
    }
    /// <summary>
    /// Patrols certain space on scene
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
                if (transform.position == walkingPoint[1].position)
                {
                    waypointIndex = 0;
                }
                if (Vector2.Distance(transform.position, player.position) < distanceToStopPatroling)
                {
                    walkRandom = false;
                    shoot = true;
                    animator.SetBool("Walk", false);
                }
            }
        }
    }
    /// <summary>
    /// Shoots projectile
    /// </summary>
    public void Shooting()
    {
        if (!shoot)
            return;
        if(shoot)
            timer += Time.deltaTime;
        if (timer >= interval)
        {
            StartCoroutine(ShootProjectile());
            timer -= interval;
        }
    }
    IEnumerator Idle()
    {
        animator.SetBool("Walk", false);
        speed = 0;
        yield return new WaitForSeconds(timeToIdle);
        speed = 0.4f;
        animator.SetBool("Walk", true);
    }

    IEnumerator ShootProjectile()
    {
        walkRandom= false;
        animator.SetBool("Walk", false);
        animator.SetTrigger("Shoot");
        yield return new WaitForSeconds(1.2f);
        Instantiate(flames, firePoint.position, firePoint.rotation);
        yield return new WaitForSeconds(0.2f);
        walkRandom = true;
        shoot= false;
        animator.SetBool("Walk", true);
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
