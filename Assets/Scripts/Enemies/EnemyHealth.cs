using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static EnemyController;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private float damageRecoveryTime = 0.5f;
    [SerializeField] private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;
    private SpriteRenderer sprite;
    private Color originalColor;
    private float flashDuration = 0.3f;
    private Vector3 originalScale;
    private int swordAndBowDamage = 1;
    private int magicArrowDamage = 2;
    private float damageScale = 0.7f;
    private float scaleRestoreDelay = 0.3f;
    private const int minHealth = 0;

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
            TakeDamageEffect(swordAndBowDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ArrowBow"))
        {
            TakeDamageEffect(swordAndBowDamage);
        }
        else if(other.gameObject.CompareTag("ArrowMagicSword"))
        {
            TakeDamageEffect(magicArrowDamage);
        }
    }

    private void TakeDamageEffect(int damage)
    {
        sprite.color = Color.red;
        transform.localScale = originalScale * damageScale; 
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
        yield return new WaitForSeconds(scaleRestoreDelay);
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
        if (currentHealth <= minHealth)
        {
            currentHealth = minHealth;
            if (enemyData.EnemyType == EnemyType.Shooter && heartPrefab != null)
            {
                Instantiate(heartPrefab, transform.position, Quaternion.identity);
            }
            Events.OnEnemyKilled?.Invoke();
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
