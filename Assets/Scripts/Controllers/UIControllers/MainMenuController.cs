using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private UIDocument PlayMenu;

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
        SceneManager.LoadScene("Boot Scene", LoadSceneMode.Additive);
    }

    private void PlayButton_clicked()
    {
        PlayMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void ControlsButton_clicked()
    {
        throw new System.NotImplementedException();
    }

    private void OptionsButton_clicked()
    {
        throw new NotImplementedException();
    }

    private void QuitButton_clicked()
    {
        throw new NotImplementedException();
    }
}
