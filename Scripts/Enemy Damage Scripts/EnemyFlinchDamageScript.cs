using FMODUnity;
using System.Collections;
using UnityEngine;

public class EnemyFlinchDamageScript : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] public Animator animator;
    [SerializeField] public new Rigidbody2D rigidbody2D;
    [SerializeField] public Transform player;
    [SerializeField] public new Collider2D collider2D;
    [Header("Script Dependency")]
    [SerializeField] public EnemyFlinch enemyScript;
    [Header("Special Effects")]
    [SerializeField] public CameraShakeEffect shakeEffect;
    [SerializeField] public GameObject hitMarks;
    [SerializeField] public GameObject bloodSplash;
    [SerializeField] public GameObject explosionEffect;
    [SerializeField] public GameObject healthBar;
    [Header("Audio")]
    [SerializeField] public StudioEventEmitter punchSFX;
    [Header("Attack Variables")]
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
            AttackPhase2();
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
        animator.SetTrigger("Hit");
        punchSFX.Play();
        rigidbody2D.velocity = Vector2.zero;
        healthBar.SetActive(true);
        hitMarks.SetActive(true);
        yield return new WaitForSeconds(moveAfterHitTime);
        hitMarks.SetActive(false);
        healthBar.SetActive(false);
    }
    IEnumerator PlayerAttackPhase2()
    {
        EnemyKnockDownDestination();
        animator.SetTrigger("Knock");
        healthBar.SetActive(true);
        rigidbody2D.gravityScale = gravityRegulator;
        yield return new WaitForSeconds(standUpFromKnockDown);
        rigidbody2D.gravityScale = 0;
        healthBar.SetActive(false);
    }
    IEnumerator PlayerJumpAttack()
    {
        animator.SetTrigger("Knock");
        bloodSplash.SetActive(true);
        healthBar.SetActive(true);
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.9f);
        bloodSplash.SetActive(false);
        healthBar.SetActive(false);
    }
    IEnumerator SuperAttack()
    {
        animator.SetTrigger("Knock");
        punchSFX.Play();
        explosionEffect.SetActive(true);
        healthBar.SetActive(true);
        if (player.position.x < transform.position.x)
        {
            rigidbody2D.AddForce(new Vector2(1, 0) * 1, ForceMode2D.Impulse);
        }
        if (player.position.x > transform.position.x)
        {
            rigidbody2D.AddForce(new Vector2(-1, 0) * 1, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.35f);
        explosionEffect.SetActive(false);
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
        collider2D.enabled= false;
        shakeEffect.isShaking = true;
        enemyScript.enabled = false;
        enemyScript.followPlayer = false;
        animator.SetTrigger("Knock");
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
    public void AttackPhase2()
    {
        animator.SetTrigger("Knock");
        if (enemyScript.followPlayer)
        {
            if (player.position.x < transform.position.x)
            {
                rigidbody2D.AddForce(new Vector2(1, 0) * 1.2f, ForceMode2D.Impulse);
            }
            if (player.position.x > transform.position.x)
            {
                rigidbody2D.AddForce(new Vector2(-1, 0) * 1.2f, ForceMode2D.Impulse);
            }
        }
        else
        {
            StartCoroutine(PlayerAttackPhase2());
        }
    }
}
