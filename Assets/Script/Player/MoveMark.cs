using UnityEngine;
using UnityEngine.AI;

public class MoveMark : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent playerAgent;
    private float threshold = 0.2f;

    public void SetTarget(Transform player, NavMeshAgent agent)
    {
        playerTransform = player;
        playerAgent = agent;
    }

    void Update()
    {
        if (playerAgent == null || playerTransform == null)
            return;

        // µµÂø ÆÇÁ¤
        if (!playerAgent.pathPending && playerAgent.remainingDistance <= threshold)
        {
            Destroy(gameObject);
        }
    }
}
