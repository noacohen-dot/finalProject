using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; 


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
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("FireBall") || other.gameObject.CompareTag("Flower"))
        {
            sprite.color = Color.red;
            TakeDamage(1);
            StartCoroutine(FlashRed());
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
        if (currentHealth <= 0)
        {
            currentHealth = 0;
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

        Time.timeScale = 0; 
        yield return new WaitForSecondsRealtime(delayBeforeReturn); 
        Time.timeScale = 1;
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

   