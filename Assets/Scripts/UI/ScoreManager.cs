using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    float delayBeforeReturn = 3f;
    int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int victoryScore = 5;
    [SerializeField] TextMeshProUGUI victoryText;
    [SerializeField] GameObject victoryPanel;

    void Start()
    {
        scoreText.text = $"Score: {score}/{victoryScore}";
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
            Time.timeScale = 0;
            victoryText.gameObject.SetActive(true);
            victoryPanel.gameObject.SetActive(true);
            victoryText.text = "Good-Job";
            StartCoroutine(ReturnToFirstScene());
        }
    }

    private IEnumerator ReturnToFirstScene()
    {
        yield return new WaitForSecondsRealtime(delayBeforeReturn); 
        Time.timeScale = 1; 
        SceneManager.LoadScene("StartScene");
    }

    private void OnDestroy()
    {
        Events.OnScoreUpdate -= UpdateScore;
    }
}
