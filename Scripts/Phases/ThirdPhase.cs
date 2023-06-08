using UnityEngine;

public class ThirdPhase : MonoBehaviour
{
    [SerializeField] public bool isPhase3 = false;
    [Header("Camera Setting")]
    [SerializeField] public CameraHandler handler;
    [Header("Enemy Spawns")]
    [SerializeField] public GameObject[] phase2Spawns;
    [Header("Scene UI")]
    [SerializeField] public GameObject goSign;
    [SerializeField] public float phase3Timer;
    private float timer = 0;
    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraHandler>();
    }

    void Update()
    {
        Phase3();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            handler.leftCameraBorder = 10.1f;
            handler.rightCameraBorder = 10.1f;
            isPhase3 = true;
            for (int j = 0; j < phase2Spawns.Length; j++)
            {
                phase2Spawns[j].SetActive(true);
            }
        }
    }

    public void Phase3()
    {
        if (isPhase3)
        {
            timer += Time.deltaTime;
            if (timer >= phase3Timer)
            {
                handler.leftCameraBorder = 6.1f;
                handler.rightCameraBorder = 16f;
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
