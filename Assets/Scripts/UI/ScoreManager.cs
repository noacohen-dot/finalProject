using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private int minimumScore = 0;
    private float pausedTimeScale = 0f;
    private float normalTimeScale = 1f;

    float delayBeforeSceneReturn = 3f;
    int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int victoryScore = 5;
    [SerializeField] TextMeshProUGUI victoryText;
    [SerializeField] GameObject victoryPanel;

    void Start()
    {
        scoreText.text = $"{score}/{victoryScore}";
        Events.OnScoreUpdate += UpdateScore;
    } 

    private void UpdateScore(int scoreAdd)
    {
        score += scoreAdd;
        if (score < minimumScore) score = minimumScore;
        scoreText.text = $"{score}/{victoryScore}";
        if (score >= victoryScore)
        {
            ShowVictoryMessage();
        }
    }

    private void ShowVictoryMessage()
    {
        if (victoryText != null)
        {
            Time.timeScale = pausedTimeScale;
            victoryText.gameObject.SetActive(true);
            victoryPanel.gameObject.SetActive(true);
            victoryText.text = "YOU-WIN!";
            StartCoroutine(ReturnToFirstScene());
        }
    }

    private IEnumerator ReturnToFirstScene()
    {
        yield return new WaitForSecondsRealtime(delayBeforeSceneReturn); 
        Time.timeScale = normalTimeScale; 
        SceneManager.LoadScene("StartScene");
    }

    private void OnDestroy()
    {
        Events.OnScoreUpdate -= UpdateScore;
    }
}
