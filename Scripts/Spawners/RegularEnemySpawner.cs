using UnityEngine;

public class RegularEnemySpawner : MonoBehaviour
{
    [SerializeField] public float spawningInterval;
    [SerializeField] public GameObject[] enemy;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawningInterval)
        {
            SpawnEnemies();
            timer -= spawningInterval;
        }
    }
    public void SpawnEnemies()
    {
        Instantiate(enemy[Random.Range(0, 4)], transform.position, transform.rotation);
    }
}
