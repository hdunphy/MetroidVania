using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField, Tooltip("GUID used by save system")] private string GUID;

    public string guid { get => GUID; }

    /// <summary>
    /// Called to start the open door animation
    /// </summary>
    public void Open()
    {
        //Animate
        Animator.enabled = true;

        //Save door state
        SceneData sceneData = SaveData.current.GetScene(gameObject.scene.name);
        int index = sceneData.SceneObjectStates.FindIndex(x => x.guid == guid);
        sceneData.SceneObjectStates[index] = new SceneObjectState { guid = guid, isOn = true };
    }

    /// <summary>
    /// Called by the animator to hide the object when it is done opening
    /// </summary>
    public void SetObjectInactive()
    {
        Animator.enabled = false;
        gameObject.SetActive(false);
    }

    public void SetIsOpen(bool _isOpen)
    {
        Animator.enabled = false;
        gameObject.SetActive(!_isOpen);
    }
}
