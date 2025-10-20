using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyController;

public class EnemyMove : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private EnemiesData enemyData;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is null");
        }
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("SpriteRenderer is null");
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * (enemyData.MoveSpeed * Time.fixedDeltaTime));
        if (moveDir.x < 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetDirection)
    {
        moveDir = targetDirection;
    }
    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }
}
