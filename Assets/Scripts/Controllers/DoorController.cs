using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : SceneObjectState
{
    [SerializeField] private Animator Animator;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Sprite DoorClosedSprite;

    private void Start()
    {
        SceneObjectData = new SceneObjectData { guid = GUID, isOn = false };
        OnLoad();
    }

    /// <summary>
    /// Called to start the open door animation
    ///     Also updates the save data with new state
    /// </summary>
    public void Open()
    {
        SceneObjectData.isOn = true;
        //Animate
        Animator.enabled = true;
    }

    /// <summary>
    /// Called by the animator to hide the object when it is done opening
    /// </summary>
    public void SetObjectInactive()
    {
        Animator.enabled = false;
        SpriteRenderer.sprite = DoorClosedSprite;
        gameObject.SetActive(false);
    }

    public override void AfterLoad()
    {
        gameObject.SetActive(!SceneObjectData.isOn);
    }

    public override void AfterUpdate()
    {
        //not used
    }
}
