using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    private Renderer rend;
    private Collider col;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(1);

            // Disable visual and collision
            rend.enabled = false;
            col.enabled = false;

            StartCoroutine(RespawnCoin());
        }
    }

    IEnumerator RespawnCoin()
    {
        yield return new WaitForSeconds(5f); // wait 5 seconds

        // Re-enable the coin
        rend.enabled = true;
        col.enabled = true;
    }
}
