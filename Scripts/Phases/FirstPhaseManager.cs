using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPhaseManager : MonoBehaviour
{
    [SerializeField] public bool isPhase1 = false;
    [Header("Camera Setting")]
    [SerializeField] public CameraHandler handler;
    [Header("Enemy Spawns")]
    [SerializeField] public GameObject[] phase1Spawns;
    [Header("Scene UI")]
    [SerializeField] public GameObject goSign;
    [SerializeField] public float phase1Timer;
    private float timer = 0;
    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraHandler>();
    }

    void Update()
    {
        Stage1Phase1();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            handler.leftCameraBorder = 2.5f;
            handler.rightCameraBorder = 2.5f;
            isPhase1 = true;
            for (int j = 0; j < phase1Spawns.Length; j++)
            {
                phase1Spawns[j].SetActive(true);
            }
        }
    }

    public void Stage1Phase1()
    {
        if (isPhase1)
        {
            timer += Time.deltaTime;
            if (timer >= phase1Timer)
            {
                handler.leftCameraBorder = 2.5f;
                handler.rightCameraBorder = 14f;
                timer = 0;
                goSign.SetActive(true);
                gameObject.SetActive(false);
                Destroy(gameObject,4);
                Invoke("DisableGoSign", 3.9f);
                for (int j = 0; j < phase1Spawns.Length; j++)
                {
                    phase1Spawns[j].SetActive(false);
                }
            }

        }
    }

    public void DisableGoSign()
    {
        goSign.SetActive(false);
    }
}
