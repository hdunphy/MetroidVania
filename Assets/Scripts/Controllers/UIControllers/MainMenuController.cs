using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private UIDocument PlayMenu;
    [SerializeField] private string BootSceneName = "Boot Scene";

    private Button PlayButton;
    private Button ControlsButton;
    private Button OptionsButton;
    private Button QuitButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        PlayButton = root.Q<Button>("play-button");
        ControlsButton = root.Q<Button>("controls-button");
        OptionsButton = root.Q<Button>("options-button");
        QuitButton = root.Q<Button>("quit-button");

        PlayButton.clicked += PlayButton_clicked;
        ControlsButton.clicked += ControlsButton_clicked;
        OptionsButton.clicked += OptionsButton_clicked;
        QuitButton.clicked += QuitButton_clicked;
    }

    private void OnDisable()
    { //do I need this?
        PlayButton.clicked -= PlayButton_clicked;
        ControlsButton.clicked -= ControlsButton_clicked;
        OptionsButton.clicked -= OptionsButton_clicked;
        QuitButton.clicked -= QuitButton_clicked;
    }

    private void Start()
    {
        if (SceneManager.GetSceneByName(BootSceneName).isLoaded)
        {
            SetButtonOptions();
        }
        else
        {
            StartCoroutine(GetBootScene());
        }

    }

    private IEnumerator GetBootScene()
    {
        yield return SceneManager.LoadSceneAsync(BootSceneName, LoadSceneMode.Additive);

        SetButtonOptions();
    }

    private void SetButtonOptions()
    {
        switch (GameSceneController.Singleton.CurrentGameState)
        {
            case GameSceneController.GameState.InGame:
                break;
            case GameSceneController.GameState.Paused:
                PlayButton.text = "Resume";
                QuitButton.text = "Exit to Menu";
                break;
            case GameSceneController.GameState.Menu:
                PlayButton.text = "Play";
                QuitButton.text = "Quit";
                break;
        }
    }

    private void PlayButton_clicked()
    {
        if(GameSceneController.Singleton.CurrentGameState == GameSceneController.GameState.Menu)
        {
            PlayMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
        else if (GameSceneController.Singleton.CurrentGameState == GameSceneController.GameState.Paused)
        {
            GameSceneController.Singleton.SetPaused(false);
        }
    }

    private void ControlsButton_clicked()
    {
        Debug.Log("Go to Controls");
    }

    private void OptionsButton_clicked()
    {
        Debug.Log("Go to Options");
    }

    private void QuitButton_clicked()
    {
        if (GameSceneController.Singleton.CurrentGameState == GameSceneController.GameState.Menu)
        {
            Debug.Log("Quitting");
            Application.Quit();
        }
        else if (GameSceneController.Singleton.CurrentGameState == GameSceneController.GameState.Paused)
        {
            GameSceneController.Singleton.QuitToMenu();
        }
    }
}
