using UnityEngine;

public class EnemyKillManager : MonoBehaviour
{
    [SerializeField] private GameObject treasureChestPrefab;
    [SerializeField] private int killsToSpawnChest = 5;
    private Vector3 chestSpawnOffset = new Vector3(1f, 1f, 0f);
    private int resetKillCount = 0;
    private int currentKills = 0;
    private Vector3 lastKnownPlayerPosition;

    private void Start()
    {
        if (treasureChestPrefab == null)
        {
            Debug.LogError("TreasureChestPrefab is null!");
        }

        Events.OnEnemyKilled += HandleEnemyKilled;
        Events.OnPlayerPositionChanged += HandlePlayerPositionChanged;
    }

    private void HandlePlayerPositionChanged(Vector3 playerPosition)
    {
        lastKnownPlayerPosition = playerPosition;
    }

    private void HandleEnemyKilled()
    {
        currentKills++;
        if (currentKills >= killsToSpawnChest)
        {
            currentKills = resetKillCount;
            SpawnTreasureChestNearPlayer();
        }
    }

    private void SpawnTreasureChestNearPlayer()
    {
        if (treasureChestPrefab == null)
        {
            Debug.LogError("Treasure chest prefab missing!");
            return;
        }

        Vector3 spawnPos = lastKnownPlayerPosition + chestSpawnOffset;
        Instantiate(treasureChestPrefab, spawnPos, Quaternion.identity);
    }

    private void OnDestroy()
    {
        Events.OnEnemyKilled -= HandleEnemyKilled;
        Events.OnPlayerPositionChanged -= HandlePlayerPositionChanged;
    }
}
