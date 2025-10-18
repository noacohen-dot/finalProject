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


    private void Start()
    {
        currentHealth = maxHealth; 
        UpdateHealthSlider();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            TakeDamage(1); 
        }
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
            healthSlider = GameObject.Find("HeartSlider").GetComponent<Slider>();
        }
        healthSlider.maxValue = maxHealth; 
        healthSlider.value = currentHealth; 
    }
}

   