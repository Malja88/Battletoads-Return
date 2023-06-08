using System.Collections;
using UnityEngine;
public enum ComboState
{
    None, Attack1, Attack2, Attack3
}
public class PlayerMovement : MonoBehaviour
{
    [Header("Enum Values")]
    private ComboState currentComboState;
    [Header("Boolean Values")]
    private bool activateTimerToReset;
    [SerializeField] public bool isAttacking;
    [SerializeField] public bool isJumping;
    [SerializeField] public bool isRunningAttack;
    [SerializeField] public bool superMove;
    [SerializeField] public bool isMoving;
    [Header("Attacking Variables")]
    [SerializeField] public float defaultComboTimer = 0.5f;
    public float tapSpeed = 0.2f;
    private float lastTapTime = 0;
    private float currentComboTimer;
    [Header("Player Movement Values")]
    private float defaultHorizontalSpeed = 45;
    private float defaultVerticalSpeed = 20;
    [SerializeField] public float runSpeed = 70;
    float horizontalMove, verticalMove;
    [SerializeField] public float attackWaitTime;
    [Header("Body Components")]
    [SerializeField] public Animator animator;
    [SerializeField] Rigidbody2D rb2D;
    [Header("Special Effects")]
    [SerializeField] public GameObject finalPunchRegulator;
    [SerializeField] public GameObject runningSmoke;
    [Header("Scripts Dependencies")]
    [SerializeField] public PlayerController controller;
    [SerializeField] public PlayerHealthScript playerHealth;
    [SerializeField] public HealthBarScript healthBar;
    private void Start()
    {
        currentComboState = ComboState.None;
        currentComboTimer = defaultComboTimer;
        lastTapTime = 0;
        isAttacking= true;
        superMove = true;
        isJumping= true;
        isRunningAttack= true;
        isMoving = true;
    }
    private void Update()
    {
        CharacterMove();
        ResetComboState();
        ComboAttacks();
        CharacterRun();
        CharachterJump();
        SuperAttack();
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
    }
    /// <summary>
    /// Move character
    /// </summary>
    private void CharacterMove()
    {
        if (!isMoving)
            return;
        if(isMoving)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
            if (Mathf.Abs(horizontalMove) >= 1 || Mathf.Abs(verticalMove) >= 1)
            {
                animator.SetBool("Walk", true);
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
    }
    /// <summary>
    /// Combo frequency regulator
    /// </summary>
    private void ResetComboState()
    {
        if (activateTimerToReset)
        {
            currentComboTimer -= Time.deltaTime;
            if (currentComboTimer <= 0)
            {
                currentComboState = ComboState.None;
                activateTimerToReset = false;
                currentComboTimer = defaultComboTimer;
            }
        }
    }
    public void SuperAttack()
    {
        if (!superMove)
            return;        
        if(superMove)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetTrigger("Super");
                playerHealth.currentHealth -= 50;
                healthBar.SetHealth(playerHealth.currentHealth);
                if(playerHealth.currentHealth < 50)
                {
                    superMove = false;
                }
            }
        }
    }
    private void CharacterRun()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            if ((Time.time - lastTapTime) < tapSpeed)
            {
                animator.SetBool("Run", true);
                runningSmoke.SetActive(true);
                controller.horizontalSpeed = runSpeed;
            }
            lastTapTime = Time.time;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            controller.horizontalSpeed = defaultHorizontalSpeed;
            animator.SetBool("Run", false);
            runningSmoke.SetActive(false);
        }
        if (!isRunningAttack)
            return;
        if(isRunningAttack)
        {
            if (Input.GetMouseButtonDown(1) && controller.horizontalSpeed == runSpeed)
            {
                animator.SetTrigger("RunAttack");
            }
        }

    }
    public void ComboAttacks()
    {
        if(!isAttacking)
        {
            return;
        }
        if(isAttacking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (currentComboState == ComboState.Attack3)
                    return;

                currentComboState++;
                activateTimerToReset = true;
                currentComboTimer = defaultComboTimer;
                if (currentComboState == ComboState.Attack1)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("Attack1");
                    }
                    StartCoroutine(Return());

                }
                if (currentComboState == ComboState.Attack2)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("Attack2");
                    }
                    StartCoroutine(Return());
                }

                if (currentComboState == ComboState.Attack3)
                {
                    StartCoroutine(TurnFinalPunchRegulator());
                    StartCoroutine(Return());
                }
            }
        }
       
    }
    private void CharachterJump()
    {
        if (!isJumping)
            return;
        if(isJumping)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump");
            }
        }
   
    }
    private IEnumerator Return()
    {
        controller.verticalSpeed = 0;
        controller.horizontalSpeed = 0;
        yield return new WaitForSeconds(attackWaitTime);
        controller.verticalSpeed = defaultVerticalSpeed;
        controller.horizontalSpeed = defaultHorizontalSpeed;
    }
    IEnumerator TurnFinalPunchRegulator()
    {
        finalPunchRegulator.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        finalPunchRegulator.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Phase4"))
        {
            StartCoroutine(BossMeeting());
        }
    }

    IEnumerator BossMeeting()
    {
        isMoving = false;
        rb2D.bodyType = RigidbodyType2D.Static;
        animator.SetBool("Fright", true);
        yield return new WaitForSeconds(3.8f);
        animator.SetBool("Fright", false);
        rb2D.bodyType = RigidbodyType2D.Dynamic;
        isMoving = true;
    }
}
