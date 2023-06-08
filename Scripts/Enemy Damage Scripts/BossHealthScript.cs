using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealthScript : MonoBehaviour
{
    [Header("Boss Health")]
    [SerializeField] public int maxHealth;
    [HideInInspector] public int currentHealth;
    [Header("Body Components")]
    [SerializeField] public Animator animator;
    [SerializeField] public Rigidbody2D rb2d;
    [SerializeField] public GameObject bossBody;
    [Header("Script Dependecies")]
    [SerializeField] public HealthBarScript barScript;
    [SerializeField] public BossAI bossAI;
    [Header("UI Components")]
    [SerializeField] public GameObject endOfRoundFX;
    [SerializeField] public GameObject stageCompleteSign;
    [SerializeField] public GameObject crossfadeToNextScene;
    [Header("Attacking Components")]
    [SerializeField] public GameObject ramAttackTriggers;
    [SerializeField] public GameObject[] cameraSpawns;
    [Header("Audio")]
    [SerializeField] public StudioEventEmitter stageMusic;
    [SerializeField] public StudioEventEmitter missionCompleteMusic;
    [SerializeField] public StudioEventEmitter missionCompleteVoice;
    private void Start()
    { 
        currentHealth = maxHealth;
        barScript.SetMaxHealth(maxHealth);
        barScript.SetHealth(currentHealth);
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        barScript.SetHealth(currentHealth);
        if(currentHealth <= maxHealth/2)
        {
            bossAI.regularSpeed = 0.8f;
            bossAI.runSpeed = 3f;
            bossAI.attackInterval = 1;
            for (int i = 0; i < cameraSpawns.Length; i++)
            {
                cameraSpawns[i].SetActive(false);
            }
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(BossDeath());
        }
    }

    IEnumerator BossDeath()
    {
        ramAttackTriggers.SetActive(false);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        foreach (var enemy in enemies)
        {
            var animator = enemy.GetComponent<Animator>();
            animator.SetTrigger("Death");
            Destroy(enemy, 2);
        }
        animator.SetTrigger("Death");
        rb2d.velocity = Vector2.zero;
        bossAI.enabled = false;
        Time.timeScale = Mathf.Lerp(1, 0.3f, 5);
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = Mathf.Lerp(0.3f, 1, 5);
        yield return new WaitForSeconds(1);
        stageMusic.Stop();
        missionCompleteMusic.Play();
        endOfRoundFX.SetActive(true);
        yield return new WaitForSecondsRealtime(0.56f);
        endOfRoundFX.SetActive(false);
        stageCompleteSign.SetActive(true);
        missionCompleteVoice.Play();
        yield return new WaitForSeconds(1);
        crossfadeToNextScene.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(bossBody);
    }
}
