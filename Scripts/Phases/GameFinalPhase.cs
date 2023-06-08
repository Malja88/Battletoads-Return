using FMODUnity;
using System.Collections;
using UnityEngine;

public class GameFinalPhase : MonoBehaviour
{
    [Header("Camera Setting")]
    [SerializeField] public CameraHandler handler;
    [Header("Boss Body Elements")]
    [SerializeField] public GameObject bossPrefab;
    [SerializeField] public GameObject bossAppearenceRay;
    [SerializeField] public BossAI bossAI;
    [SerializeField] public BoxCollider2D boxCollider;
    [Header("Audio")]
    [SerializeField] public StudioEventEmitter stageMusic;
    [SerializeField] public StudioEventEmitter bossMusic;
    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraHandler>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            stageMusic.Stop();
            bossMusic.Play();
            handler.leftCameraBorder = 16f;
            handler.rightCameraBorder = 16f;
            StartCoroutine(BossEntrance());
        }
    }

    IEnumerator BossEntrance()
    {
        boxCollider.enabled = false;
        bossAppearenceRay.SetActive(true);
        yield return new WaitForSeconds(2);
        bossPrefab.SetActive(true);
        bossAppearenceRay.SetActive(false);
        bossAI.enabled = false;
        yield return new WaitForSeconds(2);
        bossAI.enabled=true;
        Destroy(gameObject);
    }
}
