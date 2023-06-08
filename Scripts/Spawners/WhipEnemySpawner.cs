using UnityEngine;

public class WhipEnemySpawner : MonoBehaviour
{
    [SerializeField] public float spawningInterval;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public Transform[] walkingPoint;
    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= spawningInterval)
        {
            SpawnEnemies();
            timer -= spawningInterval;
        }
    }
    public void SpawnEnemies()
    {
        var newEnemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        if (newEnemy.TryGetComponent(out PatrolingScriptWithRandomAttacks enemy))
        {
            enemy.SetWalkingPoints(walkingPoint);
        }
    }   
}
