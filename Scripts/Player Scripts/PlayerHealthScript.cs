using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    [Header("Life & Health Values")]
    [SerializeField] public int maxHealth = 500;
    [SerializeField] public static int lives = 9;
    [HideInInspector] public int currentHealth;
    [Header("Body Components")]
    [SerializeField] public Animator animator;
    [SerializeField] public GameObject mainCharacter;
    [Header("Scene UI")]
    [SerializeField] public Text livesText;
    [SerializeField] public GameObject crossfade;
    [SerializeField] public GameObject gameoverScreen;
    [Header("Scripts Dependencies")]
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public HealthBarScript barScript;
    [Header("Audio")]
    [SerializeField] public StudioEventEmitter playerDeathSFX;
    [SerializeField] public StudioEventEmitter itemPickUpSFX;
    [SerializeField] public GameObject stageMusic;
    void Start()
    {
        stageMusic = GameObject.FindGameObjectWithTag("StageMusic");
        PlayerPrefs.GetInt("PlayerLives", lives);
        currentHealth = maxHealth;
        barScript.SetMaxHealth(maxHealth);
        barScript.SetHealth(currentHealth);
    }
    private void Update()
    {
        livesText.text = lives.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyHitBox"))
        {
            HurtAnimationCompilation(Random.Range(0,3));
        }
        if(collision.CompareTag("FatalHitBox") && currentHealth > 50)
        {
            animator.SetTrigger("Knock");
        }
        if(collision.CompareTag("Chicken"))
        {
            itemPickUpSFX.Play();
            Destroy(collision.gameObject);
            currentHealth = maxHealth;
            barScript.SetHealth(currentHealth);
        }
        if(collision.CompareTag("Apple"))
        {
            itemPickUpSFX.Play();
            Destroy(collision.gameObject);
            if (currentHealth >= maxHealth - 100)
            {
                currentHealth = maxHealth;
                barScript.SetHealth(currentHealth);
            }
            if(currentHealth <= maxHealth - 100)
            {
                currentHealth += 100;
                barScript.SetHealth(currentHealth);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        barScript.SetHealth(currentHealth);
        if (currentHealth <= 0 && lives >= 2)
        {
            PlayerDeath();
        }
        else if(currentHealth <= 0 && lives <= 1)
        {
            PlayerCompleteDeath();
        }
    }

    public void PlayerDeath()
    {
        stageMusic.SetActive(false);
        playerDeathSFX.Play();
        animator.SetTrigger("Death");
        playerMovement.enabled= false;
        PlayerPrefs.SetInt("PlayerLives", lives);
        Destroy(mainCharacter,3.5f);
        crossfade.SetActive(true);
        Invoke("Restart", 3.4f);       
    }
    public void PlayerCompleteDeath()
    {
        stageMusic.SetActive(false);
        playerDeathSFX.Play();
        animator.SetTrigger("Death");
        playerMovement.enabled = false;
        PlayerPrefs.DeleteAll();
        crossfade.SetActive(true);
        Invoke("GameOver", 3.4f);
        Invoke("ToMainMenu", 5);
    }

    public void Restart()
    {
        lives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameOver()
    {
        lives = 9;
        gameoverScreen.SetActive(true);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void HurtAnimationCompilation(int hurt)
    {
        if (hurt == 0)
        {
            animator.SetTrigger("Hurt");
        }

        if (hurt == 1)
        {
            animator.SetTrigger("Hurt2");
        }
        if (hurt == 2)
        {
            animator.SetTrigger("Hurt3");
        }
    }
}
