using UnityEditor.PackageManager;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    [SerializeField] EnemiesData enemiesData;
    public float delayForDestroy = 1f;
    SpriteRenderer sprite;
    private bool isDead = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("SpriteRenderer is null");
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(isDead==true) {
            return;
        }
        else if (other.gameObject.CompareTag("Sword"))
        {
            isDead = true;
            transform.localScale = transform.localScale * 0.5f;
            sprite.color = Color.red;
            Events.OnScoreUpdate?.Invoke(enemiesData.Score);
            Destroy(gameObject, delayForDestroy);
            return;
        }
    }
} 
