using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    float delayBeforeReturn = 4f;
    int score = 0;
    TextMeshProUGUI scoreText;
    public int victoryScore = 15;
    [SerializeField] TextMeshProUGUI victoryText;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        if (scoreText == null)
        {
            Debug.LogError("TextMeshProUGUI is null");
        }
        scoreText.text = $"Score: {score}/{victoryScore}";
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false);
        }
        Events.OnScoreUpdate += UpdateScore;
    }

    private void UpdateScore(int scoreAdd)
    {
        score += scoreAdd;
        if (score < 0) score = 0;
        scoreText.text = $"Score: {score}/{victoryScore}";
        if (score >= victoryScore)
        {
            ShowVictoryMessage();
        }
    }

    private void ShowVictoryMessage()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(true);
            victoryText.text = "Good-job!!";
            StartCoroutine(ReturnToFirstScene());
        }
    }

    private IEnumerator ReturnToFirstScene()
    {
        float timer = 0f;
        while (timer < delayBeforeReturn)
        {
            timer += Time.deltaTime; 
            yield return null;     
        }
        SceneManager.LoadScene("StartScene");
    }

    private void OnDestroy()
    {
        Events.OnScoreUpdate -= UpdateScore;
    }
}
