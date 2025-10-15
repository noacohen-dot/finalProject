using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceenUIManger : MonoBehaviour
{
    [SerializeField] string firstLevelName = "Level1";
    [Header("Buttons")]
    [SerializeField] Button startButton;
    [SerializeField] Button exitButton;

    [Header("Panels")]
    [SerializeField] GameObject buttonsPanel;


    void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
        AddButtonsListenrs();
    }

    private void AddButtonsListenrs()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene(firstLevelName));
        exitButton.onClick.AddListener(() => Application.Quit());
    }
}
