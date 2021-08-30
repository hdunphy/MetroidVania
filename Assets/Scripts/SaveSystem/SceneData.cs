using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SceneData
{
    public string SceneName;
    public List<SceneObjectState> SceneObjectStates;

    public SceneData(string _sceneName)
    {
        SceneName = _sceneName;
        SceneObjectStates = new List<SceneObjectState>();
    }
}
