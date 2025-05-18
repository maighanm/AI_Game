using UnityEngine;
using UnityEngine.AI;

public class StandBy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Transform player;
    private NavMeshAgent agent;
    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true; // Initially idle
    }

    void Update()
    {
        if (ScoreManager.instance == null || player == null)
            return;

        // Start chasing after score reaches 150
        if (!isChasing && ScoreManager.instance.score >= 150)
        {
            isChasing = true;
            agent.isStopped = false;
        }

        if (isChasing)
        {
            agent.SetDestination(player.position);
        }
    }
}
