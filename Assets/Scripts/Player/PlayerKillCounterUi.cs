using TMPro;
using UnityEngine;

public class PlayerKillCounterUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killText;
    [SerializeField] private int killsToTreasure = 10;
    [SerializeField] private int currentKills = 0;
    private int resetKillCountValue = 0;

    private void Start()
    {
        Events.OnEnemyKilled += HandleEnemyKilled;
        Events.OnTreasureCollected += HandleTreasureCollected;
        UpdateText();
    }

    private void HandleEnemyKilled()
    {
        currentKills++;
        if (currentKills > killsToTreasure)
            currentKills = killsToTreasure; 

        UpdateText();
    }

    private void HandleTreasureCollected()
    {
        currentKills = resetKillCountValue;
        UpdateText();
    }

    private void UpdateText()
    {
        killText.text = $"{currentKills}/{killsToTreasure}";
    }

    private void OnDestroy()
    {
        Events.OnEnemyKilled -= HandleEnemyKilled;
        Events.OnTreasureCollected -= HandleTreasureCollected;
    }
}

