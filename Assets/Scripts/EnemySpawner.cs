using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public float spawnDelay = 30f;

    void Start()
    {
        if (spawnPoints.Length > 0)
        {
            Invoke("SpawnEnemies", spawnDelay);
        }
        else
        {
            Debug.LogWarning("No spawn points assigned in EnemySpawner.");
        }
    }

    void SpawnEnemies()
    {
        foreach (Transform point in spawnPoints)
        {
            Instantiate(enemyPrefab, point.position, Quaternion.identity);
        }
    }
}



