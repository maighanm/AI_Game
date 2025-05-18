using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public bool hasSpawned = false;

    public int spawnAtScore = 150; // Score threshold to trigger spawn

    void Update()
    {
        // Check if score condition is met and enemies haven't spawned yet
        if (!hasSpawned && ScoreManager.instance != null && ScoreManager.instance.score >= spawnAtScore)
        {
            SpawnEnemies();
            hasSpawned = true;
        }
    }

    void SpawnEnemies()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points assigned in EnemySpawner.");
            return;
        }

        foreach (Transform point in spawnPoints)
        {
            Instantiate(enemyPrefab, point.position, Quaternion.identity);
        }
    }
}
