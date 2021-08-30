using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_LoadController : MonoBehaviour
{
    [SerializeField] private string saveName;
    public void OnSavePressed()
    {
        var success = SerializationManager.Save(saveName, SaveData.current);

        Debug.Log($"Save succeeded? {success}");
    }

    public void OnLoadPressed()
    {
        string path = Application.persistentDataPath + "/saves/" + saveName + ".save";
        SaveData.current = (SaveData)SerializationManager.Load(path);

        Debug.Log("Loaded");

        FindObjectOfType<PlayerController>().OnLoad(SaveData.current.PlayerPosition);
    }
}
