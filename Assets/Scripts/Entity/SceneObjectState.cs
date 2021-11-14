using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SceneObjectState : MonoBehaviour
{
    [SerializeField, Tooltip("GUID used by save system")] protected string GUID;
    protected SceneObjectData SceneObjectData { get; set; }

    //private void OnValidate()
    //{
    //    if(FindObjectsOfType<SceneObjectState>().Any(x => x.GUID == GUID && x != this))
    //    {
    //        var duplicate = FindObjectsOfType<SceneObjectState>().First(x => x.GUID == GUID && x != this);
    //        Debug.LogWarning($"Two Objects with the same GUID: {GUID}\nOn Object [{name}] and [{duplicate.name}].");
    //    }
    //}

    /// <summary>
    /// Used by SaveSystem. Updates the Scene Object Data in the Current SaveData Object for the current scene.
    ///     **Assumes that the SceneObjectData has been updated before calling**
    /// </summary>
    public virtual void OnUpdateState(string sceneName)
    {
        SaveData.current.SaveSceneObject(sceneName, SceneObjectData);

        if(!SerializationManager.Save(SaveData.current.SaveName, SaveData.current))
        {
            Debug.LogWarning("Not saved");
        }

        AfterUpdate();
    }

    /// <summary>
    /// Using the Save System. Loads the scene object data from the SceneData stored in the SaveData object.
    /// </summary>
    public virtual void OnLoad()
    {
        SceneData _scene = SaveData.current.GetScene(gameObject.scene.name);
        if (_scene.SceneObjectDatas.Any(x => x.guid == GUID)) //If this object already exists in the save data => scene data 
        {
            SceneObjectData = _scene.SceneObjectDatas.First(x => x.guid == GUID);
        }
        else
        { //else need to add the scene object data
            _scene.SceneObjectDatas.Add(SceneObjectData);
        }

        AfterLoad();
    }

    /// <summary>
    /// Abstract function called after finished loading the object's state
    /// </summary>
    public abstract void AfterLoad();

    /// <summary>
    /// Abstract function called after finished saving the object's state
    /// </summary>
    public abstract void AfterUpdate();
}
