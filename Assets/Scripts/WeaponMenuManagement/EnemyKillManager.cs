using UnityEngine;

public class EnemyKillManager : MonoBehaviour
{
    [SerializeField] private GameObject treasureChestPrefab;
    [SerializeField] private int killsToSpawnChest = 5;
    private Vector3 chestSpawnOffset = new Vector3(1f, 1f, 0f); 
    [SerializeField] private Transform playerTransform;

    private int currentKills = 0;

    private void Start()
    {
        if (playerTransform == null)
        {
            var playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
            else
                Debug.LogWarning("EnemyKillManager: לא נמצא Player עם Tag 'Player' — יש לשבץ ידנית ב-Inspector.");
        }
        if (treasureChestPrefab == null)
        {
            Debug.LogError("treasureChestPrefab is null");
        }
        Events.OnEnemyKilled += HandleEnemyKilled;
    }
    private void HandleEnemyKilled()
    {
        currentKills++;
        if (currentKills >= killsToSpawnChest)
        {
            currentKills = 0;
            SpawnTreasureChestNearPlayer();
        }
    }

    private void SpawnTreasureChestNearPlayer()
    {
        Vector3 spawnPos = playerTransform.position + chestSpawnOffset;
        Instantiate(treasureChestPrefab, spawnPos, Quaternion.identity);
    }
    private void OnDestroy()
    {
        Events.OnEnemyKilled -= HandleEnemyKilled;
    }

}

