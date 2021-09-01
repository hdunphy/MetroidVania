using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public string SceneName; //Name of the scene to save the states
    public List<SceneObjectData> SceneObjectDatas; //List of scene objects that need their states saved by the SaveSystem

    public SceneData(string _sceneName)
    {
        SceneName = _sceneName;
        SceneObjectDatas = new List<SceneObjectData>();
    }
}
