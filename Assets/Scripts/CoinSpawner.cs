using UnityEngine;
using UnityEngine.AI;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spacing = 1.5f;

    private Vector3 spawnAreaMin;
    private Vector3 spawnAreaMax;

    public GameObject mazeObject;

    void Start()
    {
        SetSpawnAreaFromMaze();
        SpawnCoinsInGrid();
    }

    void SetSpawnAreaFromMaze()
    {
        if (mazeObject != null)
        {
            MeshRenderer[] renderers = mazeObject.GetComponentsInChildren<MeshRenderer>();

            if (renderers.Length > 0)
            {
                Bounds combinedBounds = renderers[0].bounds;
                foreach (var r in renderers)
                {
                    combinedBounds.Encapsulate(r.bounds);
                }

                spawnAreaMin = combinedBounds.min;
                spawnAreaMax = combinedBounds.max;
            }
        }
    }

    void SpawnCoinsInGrid()
    {
        int coinCount = 0;

        for (float x = spawnAreaMin.x; x <= spawnAreaMax.x; x += spacing)
        {
            for (float z = spawnAreaMin.z; z <= spawnAreaMax.z; z += spacing)
            {
                Vector3 testPosition = new Vector3(x, 0.5f, z);

                NavMeshHit hit;
                if (NavMesh.SamplePosition(testPosition, out hit, 1.5f, NavMesh.AllAreas))
                {
                    Instantiate(coinPrefab, hit.position, Quaternion.identity);
                    coinCount++;
                }
            }
        }

        Debug.Log("Total Coins Spawned: " + coinCount);
    }
}
