using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //Singleton pattern
    private static SaveData _current;
    public static SaveData current
    {
        get
        {
            if (_current == null)
            {
                _current = new SaveData();
            }
            return _current;
        }
        set { _current = value; }
    }

    //Keep track of the players current ability ids
    private List<string> _PlayerHeldAbilityIds;
    public List<string> PlayerHeldAbilityIds
    {
        get { return _PlayerHeldAbilityIds; }

        set { _PlayerHeldAbilityIds = value; }
    }

    public List<SceneData> LoadedScenes; //Keep track of all loaded scenes visited by the player
    public string PlayerSceneName; //The current scene the player is on last save
    public Vector3 PlayerPosition; //The current player position on last save
    public string SaveName; //The name of the current save

    public SaveData()
    {
        LoadedScenes = new List<SceneData>();
        _PlayerHeldAbilityIds = PlayerAbilityManager.Singleton.GetStartingAbilities();
        SaveName = "save1"; //Set here for now, but will need to set from Menu UI eventually
    }

    /// <summary>
    /// Try to find the scene with the given name.
    ///     If the scene doesn't exist than add the scene to the list
    /// </summary>
    /// <param name="name">string: name of the scene being searched for</param>
    /// <returns>Scene data of the scene with the given name</returns>
    public SceneData GetScene(string name)
    {
        SceneData data;
        if (LoadedScenes.Any(x => x.SceneName == name))
        {
            data = LoadedScenes.First(x => x.SceneName == name);
        }
        else
        {
            data = new SceneData(name);
            LoadedScenes.Add(data);
        }

        return data;
    }
}
