using FMODUnity;
using System.Collections;
using UnityEngine;

public class ShootingEnemyAIDamage : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] public Animator animator;
    [SerializeField] public new Rigidbody2D rigidbody2D;
    [SerializeField] public Transform player;
    [Header("Script Dependency")]
    [SerializeField] public ShootingEnemyAI enemyScript;
    [Header("Special Effects")]
    [SerializeField] public CameraShakeEffect shakeEffect;
    [SerializeField] public GameObject hitMarks;
    [SerializeField] public GameObject bloodSplash;
    [SerializeField] public GameObject explosionEffect;
    [SerializeField] public GameObject healthBar;
    [Header("Audio")]
    [SerializeField] public StudioEventEmitter punchSFX;
    [Header("Attacking Variables")]
    [SerializeField] public float knockPower = 4;
    [SerializeField] public float moveAfterHitTime = 0.6f;
    [SerializeField] public float knockDownForce;
    [SerializeField] public float standUpFromKnockDown;
    [SerializeField] public float gravityRegulator;
    private Vector2 knockBackwards = new(-1, 1);
    private Vector2 knockForward = new(1, 1);
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shakeEffect = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraShakeEffect>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox1"))
        {
            StartCoroutine(PlayerAttack());
        }
        if (collision.CompareTag("HitBox2"))
        {
            StartCoroutine(PlayerAttackPhase2());
        }
        if (collision.CompareTag("HitBox3"))
        {
            StartCoroutine(PlayerJumpAttack());
        }
        if (collision.CompareTag("FinalHitBox"))
        {
            FinalAttack();
        }
        if (collision.CompareTag("SuperHitBox"))
        {
            StartCoroutine(SuperAttack());
        }
    }

    IEnumerator PlayerAttack()
    {
        punchSFX.Play();
        animator.SetTrigger("Hit");
        healthBar.SetActive(true);
        rigidbody2D.velocity = Vector2.zero;
        hitMarks.SetActive(true);
        yield return new WaitForSeconds(moveAfterHitTime);
        hitMarks.SetActive(false);
        healthBar.SetActive(false);
    }

    IEnumerator SuperAttack()
    {
        punchSFX.Play();
        animator.SetTrigger("Death");
        explosionEffect.SetActive(true);
        healthBar.SetActive(true);
        if (player.position.x < transform.position.x)
        {
            rigidbody2D.AddForce(new Vector2(1, 0) * 2, ForceMode2D.Impulse);
        }
        if (player.position.x > transform.position.x)
        {
            rigidbody2D.AddForce(new Vector2(-1, 0) * 2, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.35f);
        explosionEffect.SetActive(false);
        healthBar.SetActive(false);
    }
    IEnumerator PlayerAttackPhase2()
    {
        EnemyKnockDownDestination();
        healthBar.SetActive(true);
        rigidbody2D.gravityScale = gravityRegulator;
        yield return new WaitForSeconds(standUpFromKnockDown);
        rigidbody2D.gravityScale = 0;
        healthBar.SetActive(false);
    }

    IEnumerator PlayerJumpAttack()
    {
        animator.SetTrigger("Hit");
        bloodSplash.SetActive(true);
        healthBar.SetActive(true);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.9f);
        bloodSplash.SetActive(false);
        healthBar.SetActive(false);
    }

    public void EnemyKnockDownDestination()
    {
        if (player.position.x < transform.position.x)
        {
            rigidbody2D.AddForce(knockForward * knockDownForce, ForceMode2D.Impulse);
        }
        if (player.position.x > transform.position.x)
        {
            rigidbody2D.AddForce(knockBackwards * knockDownForce, ForceMode2D.Impulse);
        }
    }
    public void FinalAttack()
    {
        shakeEffect.isShaking = true;
        enemyScript.enabled = false;
        Destroy(gameObject, 1);
        if (player.position.x < transform.position.x)
        {
            rigidbody2D.AddForce(knockForward * knockPower, ForceMode2D.Impulse);
        }
        if (player.position.x > transform.position.x)
        {
            rigidbody2D.AddForce(knockBackwards * knockPower, ForceMode2D.Impulse);
        }
    }
}
