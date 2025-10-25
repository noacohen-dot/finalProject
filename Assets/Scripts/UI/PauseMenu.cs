using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private float pausedTimeScale = 0f;
    private float normalTimeScale = 1f;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = pausedTimeScale;
    }
    public void Home()
    {
        SceneManager.LoadScene("StartScene");
        Time.timeScale = normalTimeScale;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = normalTimeScale;
    }
}
