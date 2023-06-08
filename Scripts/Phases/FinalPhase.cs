using FMODUnity;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalPhase : MonoBehaviour
{
    [Header("Camera Setting")]
    [SerializeField] public CameraHandler handler;
    [Header("Scene UI")]
    [SerializeField] public GameObject endOfRoundFX;
    [SerializeField] public GameObject stageCompletedSign;
    [SerializeField] public GameObject endOfStageCrossfade;
    [Header("Enemy Spawn Points")]
    [SerializeField] public GameObject[] phase3Spawns;
    [SerializeField] public GameObject[] cameraSpawns;
    [SerializeField] public RegularEnemySpawner[] enemySpawner;
    [Header("Audio")]
    [SerializeField] public StudioEventEmitter stageSound;
    [SerializeField] public StudioEventEmitter missionCompleteSound;
    [SerializeField] public StudioEventEmitter missionCompleteVoice;
    [Header("Phase Variables")]
    [SerializeField] public float phase3EnemyIntervalRegulator = 8;
    [SerializeField] public bool isPhase3 = false;
    [SerializeField] public float phase3Timer;
    private float timer = 0;
    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraHandler>();
    }

    void Update()
    {
        Stage1Phase3();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            handler.leftCameraBorder = 14f;
            handler.rightCameraBorder = 14f;
            isPhase3 = true;
            for (int j = 0; j < phase3Spawns.Length; j++)
            {
                phase3Spawns[j].SetActive(true);
            }
            for (int i = 0; i < enemySpawner.Length; i++)
            {
                enemySpawner[i].spawningInterval = phase3EnemyIntervalRegulator;
            }
        }
    }

    public void Stage1Phase3()
    {
        if (isPhase3)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer >= phase3Timer)
            {
                for (int j = 0; j < phase3Spawns.Length; j++)
                {
                    phase3Spawns[j].SetActive(false);
                }
                for (int i = 0; i < cameraSpawns.Length; i++)
                {
                    cameraSpawns[i].SetActive(false);
                }
                StartCoroutine(EndOfStage());
                timer = 0;
            }
        }
    }

    IEnumerator EndOfStage()
    {
        stageSound.Stop();
        missionCompleteSound.Play();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        endOfRoundFX.SetActive(true);
        yield return new WaitForSeconds(1f);
        foreach (var enemy in enemies)
        {
            var animator = enemy.GetComponent<Animator>();
            animator.SetTrigger("Death");
        }
        stageCompletedSign.SetActive(true);
        missionCompleteVoice.Play();
        endOfRoundFX.SetActive(false);
        yield return new WaitForSeconds(1f);
        endOfStageCrossfade.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
