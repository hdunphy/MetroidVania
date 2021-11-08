using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayMenuController : MonoBehaviour
{
    [SerializeField] private UIDocument MainMenu;

    private List<SaveGameController> SaveGames;
    private Button BackButton;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        BackButton = root.Q<Button>("back-button");
        BackButton.clicked += BackButton_clicked;

        SaveGames = new List<SaveGameController>();
        string path = Application.persistentDataPath + "/saves/";

        DirectoryInfo dir = new DirectoryInfo(path);
        FileInfo[] saveFiles = dir.GetFiles("*.save");

        int i;
        for(i = 0; i < saveFiles.Length; i++)
        {
            if (i > 2) break;

            SaveGames.Add(new SaveGameController(saveFiles[i], root.Q($"save-game{i}")));
        }
        for(int j = i; j < 3; j++)
        {
            SaveGames.Add(new SaveGameController(null, root.Q($"save-game{j}"), true));
        }

    }

    private void BackButton_clicked()
    {
        MainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
