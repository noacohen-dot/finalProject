using System;
using System.Linq;
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
        AssignNamedActionTransition();
    }
    private void AssignNamedActionTransition()
    {
        var transitions = FindObjectsByType<NamedActionTransition>(FindObjectsSortMode.None);
        var buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        foreach (var transition in transitions)
        {
            var selectedButton = buttons.FirstOrDefault(x => x.name.Equals(transition.actionName));
            if (selectedButton != null)
            {
                selectedButton.onClick.AddListener(transition.DoAction);
            }
        }
    }

    private void AddButtonsListenrs()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene(firstLevelName));
        exitButton.onClick.AddListener(() => Application.Quit());
    }

}
