using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Save_LoadController : MonoBehaviour
{
    [SerializeField] private string saveName;
    public void OnSavePressed()
    {
        SaveData.current.SaveName = saveName;
        var success = SerializationManager.Save(saveName, SaveData.current);

        Debug.Log($"Save succeeded? {success}");
    }

    /// <summary>
    /// Called by Load Button from UI
    /// </summary>
    public void OnLoadPressed()
    {
        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
        SaveData.current = (SaveData)SerializationManager.Load(path);

        Debug.Log("Loaded");

        FindObjectOfType<PlayerController>().OnLoad(SaveData.current.PlayerPosition);

        foreach (var sceneObject in FindObjectsOfType<SceneObjectState>())
        {
            sceneObject.OnLoad();
        }
    }
}
