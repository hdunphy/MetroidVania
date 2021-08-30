using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
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

    private List<string> _PlayerHeldAbilityIds;
    public List<string> PlayerHeldAbilityIds
    {
        get { return _PlayerHeldAbilityIds; }

        set { _PlayerHeldAbilityIds = value; }
    }

    public string PlayerSceneName;
    public Vector3 PlayerPosition;
    public string SaveName;

    public SaveData()
    {
        _PlayerHeldAbilityIds = PlayerAbilityManager.Singleton.GetStartingAbilities();
        SaveName = "save1"; //Set here for now, but will need to set from Menu UI eventually
    }
}
