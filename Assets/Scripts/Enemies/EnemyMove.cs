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
    private float pushBackForce = 0.2f;
    private int flipThresholdX = 0;


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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("enemy") || other.collider.CompareTag("boundaryWall"))
        {
            Vector2 pushDir = other.contacts[0].normal;
            rb.MovePosition(rb.position + pushDir * pushBackForce);
            StopMoving();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * (enemyData.MoveSpeed * Time.fixedDeltaTime));
        if (moveDir.x < flipThresholdX)
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
