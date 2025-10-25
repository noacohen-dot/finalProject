using UnityEngine;
using System.Collections;


public class ChaserEnemy : MonoBehaviour, IEnemy
{
    private EnemyMove enemyMove;
    private Color originalColor;
    SpriteRenderer sprite;
    private float flashDuration = 0.5f;
    private Vector3 lastKnownPlayerPosition;

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        if (enemyMove == null)
        {
            Debug.LogError("EnemyMove is null!");
        }
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("SpriteRenderer is null");
        }
        originalColor = sprite.color;
        Events.OnPlayerPositionChanged += HandlePlayerPositionChanged;
    }
    private void HandlePlayerPositionChanged(Vector3 newPosition)
    {
        lastKnownPlayerPosition = newPosition;
    }

    public void Attack()
    {
        if (lastKnownPlayerPosition == Vector3.zero)
            return;
        Vector2 direction = (lastKnownPlayerPosition - transform.position).normalized;
        enemyMove.MoveTo(direction);
        sprite.color = Color.blue;
        StartCoroutine(FlashRed());
    }
    private IEnumerator FlashRed()
    {
        sprite.color = Color.blue;
        yield return new WaitForSeconds(flashDuration);
        sprite.color = originalColor;
    }
    private void OnDestroy()
    {
        Events.OnPlayerPositionChanged -= HandlePlayerPositionChanged;

    }
}
