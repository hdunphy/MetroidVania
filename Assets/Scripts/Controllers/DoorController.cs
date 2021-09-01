using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Sprite DoorClosedSprite;

    /// <summary>
    /// Called to start the open door animation
    ///     Also updates the save data with new state
    /// </summary>
    public void Open()
    {
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

    /// <summary>
    /// Changes the door state depending on Loading conditions.
    ///     If isOpen is true than the door should not be visible
    ///     Else if isOpen is false and the door is closed it should be visible and blocking the path
    /// </summary>
    /// <param name="_isOpen">bool if the door is open or closed</param>
    //public void SetIsOpen(bool _isOpen)
    //{
    //    Animator.enabled = false; //Disable the animator so it can't open the door
    //    gameObject.SetActive(!_isOpen);
    //}
}
