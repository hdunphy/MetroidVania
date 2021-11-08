using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SaveGameController
{
    private VisualElement Root;
    private FileInfo FileInfo;
    private string FileName;
    private Button StartButton;
    private Button DeleteButton;
    private bool IsNewFile;
    private const string MenuScene = "MainMenu";

    public SaveGameController(FileInfo _fileInfo, VisualElement _root, bool _isNewFile = false)
    {
        FileInfo = _fileInfo;
        FileName = _isNewFile ? "empty" : _fileInfo.Name.Replace(".save", "");
        Root = _root;
        IsNewFile = _isNewFile;

        SetUpUI();
    }

    //Destructor
    ~SaveGameController()
    {
        ResetButtonEvents();
    }

    private void ResetButtonEvents()
    {
        StartButton.clicked -= StartButton_clicked;
        DeleteButton.clicked -= DeleteButton_clicked;
    }

    private void SetUpUI()
    {
        Root.Q<Label>("save-name").text = FileName;

        StartButton = Root.Q<Button>("start-button");
        StartButton.text = IsNewFile ? "Start" : "Continue";
        StartButton.clicked += StartButton_clicked;

        DeleteButton = Root.Q<Button>("delete-button");
        DeleteButton.visible = !IsNewFile;
        DeleteButton.clicked += DeleteButton_clicked;
    }

    private void StartButton_clicked()
    {
        Debug.Log("Clicked start button for: " + FileName);
        SaveData.current.SaveName = Root.name;

        if (IsNewFile)
        {
            var success = SerializationManager.Save(Root.name, SaveData.current);

            Debug.Log($"Save succeeded? {success}");
        }

        SceneManager.UnloadSceneAsync(MenuScene);
        GameSceneController.Singleton.StartGame();
    }

    private void DeleteButton_clicked()
    {
        File.Delete(FileInfo.FullName);

        ResetButtonEvents();

        FileName = "empty";
        IsNewFile = true;

        SetUpUI();
    }
}
