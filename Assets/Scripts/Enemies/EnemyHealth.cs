using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static EnemyController;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private SpriteRenderer sprite;
    private Color originalColor;
    private float flashDuration = 0.3f;
    private Vector3 originalScale;

    [Header("Enemy Settings")]
    [SerializeField] private EnemiesData enemyData;


    private void Start()
    {
        currentHealth = enemyData.MaxHealth;
        UpdateHealthSlider();

        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("SpriteRenderer is null");
        }

        originalColor = sprite.color;
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            TakeDamageEffect(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ArrowBow"))
        {
            TakeDamageEffect(1);
        }
        else if(other.gameObject.CompareTag("ArrowMagicSword"))
        {
            TakeDamageEffect(2);
        }
    }

    private void TakeDamageEffect(int damage)
    {
        sprite.color = Color.red;
        transform.localScale = originalScale * 0.7f; 
        TakeDamage(damage);
        StartCoroutine(FlashRed());
        StartCoroutine(RestoreScale());
    }

    private IEnumerator FlashRed()
    {
        yield return new WaitForSeconds(flashDuration);
        sprite.color = originalColor;
    }

    private IEnumerator RestoreScale()
    {
        yield return new WaitForSeconds(0.3f);
        transform.localScale = originalScale;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!canTakeDamage) return;

        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfEnemyDead();
    }

    private void CheckIfEnemyDead()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (enemyData.EnemyType == EnemyType.Shooter && heartPrefab != null)
            {
                Instantiate(heartPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        healthSlider.maxValue = enemyData.MaxHealth;
        healthSlider.value = currentHealth;
    }
}
