using UnityEngine;
using System.Collections;


public class ChaserEnemy : MonoBehaviour, IEnemy
{
    private Transform target;
    private PlayerMove player;
    private EnemyMove enemyMove;
    private Color originalColor;
    SpriteRenderer sprite;
    private float flashDuration = 0.5f;

    private void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        if (enemyMove == null)
        {
            Debug.LogError("EnemyMove is null!");
        }

        player = FindFirstObjectByType<PlayerMove>();
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("PlayerMove is null!");
        }
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("SpriteRenderer is null");
        }
        originalColor = sprite.color;
    }

    public void Attack()
    {
        if (target == null || enemyMove == null) return;
        Vector2 direction = (target.position - transform.position).normalized;
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
}
