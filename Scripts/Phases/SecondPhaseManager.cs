using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPhaseManager : MonoBehaviour
{
    [SerializeField] public bool isPhase2 = false;
    [Header("Camera Setting")]
    [SerializeField] public CameraHandler handler;
    [Header("Enemy Spawns")]
    [SerializeField] public GameObject[] phase2Spawns;
    [Header("Scene UI")]
    [SerializeField] public GameObject goSign;
    [SerializeField] public float phase2Timer;
    private float timer = 0;
    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraHandler>();
    }

    void Update()
    {
        Stage1Phase2();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            handler.leftCameraBorder = 6.1f;
            handler.rightCameraBorder = 6.1f;
            isPhase2 = true;
            for (int j = 0; j < phase2Spawns.Length; j++)
            {
                phase2Spawns[j].SetActive(true);
            }
        }
    }

    public void Stage1Phase2()
    {
        if (isPhase2)
        {
            timer += Time.deltaTime;
            if (timer >= phase2Timer)
            {
                handler.leftCameraBorder = 6.1f;
                handler.rightCameraBorder = 14f;
                timer = 0;
                goSign.SetActive(true);
                gameObject.SetActive(false);
                Destroy(gameObject, 4);
                Invoke("DisableGoSign", 3.9f);
                for (int j = 0; j < phase2Spawns.Length; j++)
                {
                    phase2Spawns[j].SetActive(false);
                }
            }

        }
    }
    public void DisableGoSign()
    {
        goSign.SetActive(false);
    }
}
