using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 4;
    [SerializeField] private float delayBeforeReturn = 3f; 
    [SerializeField] private float damageRecoveryTime = 1f;
    [SerializeField] private TextMeshProUGUI deathText;
    private Slider healthSlider; 
    private int currentHealth;
    private bool canTakeDamage = true;
    [SerializeField] GameObject deathPanel;
    SpriteRenderer sprite;
    private Color originalColor;
    private float flashDuration = 0.5f;
    private int defaultDamage = 1;
    private int defaultHealAmount = 1;
    private int zeroHealth = 0;
    private float pausedTimeScale = 0f;
    private float normalTimeScale = 1f;

    private void Start()
    {
        currentHealth = maxHealth; 
        UpdateHealthSlider();
        sprite = GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError("SpriteRenderer is null");
        }
        originalColor = sprite.color;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            sprite.color = Color.red;
            TakeDamage(defaultDamage);
            StartCoroutine(FlashRed());
        }
        
    }
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthSlider();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Flower"))
        {
            sprite.color = Color.red;
            TakeDamage(defaultDamage);
            StartCoroutine(FlashRed());
        }
        else if (other.gameObject.CompareTag("Heart"))
        {
            HealPlayer(defaultHealAmount);
            Destroy(other.gameObject);
        }
    }
    
    private IEnumerator FlashRed()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        sprite.color = originalColor;
    }

    public void TakeDamage(int damageAmount)
    {
        if (!canTakeDamage) return;

        canTakeDamage = false; 
        currentHealth -= damageAmount; 
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

    private void CheckIfPlayerDeath()
    {
        if (currentHealth <= zeroHealth)
        {
            currentHealth = zeroHealth;
            StartCoroutine(HandlePlayerDeath());
        }
    }
    private IEnumerator HandlePlayerDeath()
    {
        if (deathText != null )
        {
            deathText.gameObject.SetActive(true);
            deathPanel.SetActive(true);
            deathText.text = "GAME OVER";
            Debug.Log("player death");
        }

        Time.timeScale = pausedTimeScale; 
        yield return new WaitForSecondsRealtime(delayBeforeReturn); 
        Time.timeScale = normalTimeScale;
        SceneManager.LoadScene("StartScene");
    }


    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("HeartSliderPlayer").GetComponent<Slider>();
        }
        healthSlider.maxValue = maxHealth; 
        healthSlider.value = currentHealth; 
    }
}

   