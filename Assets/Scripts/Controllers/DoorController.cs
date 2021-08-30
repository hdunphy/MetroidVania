using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    //private bool isOpen;

    /// <summary>
    /// Called to start the open door animation
    /// </summary>
    public void Open()
    {
        //Animate
        Animator.enabled = true;
        //isOpen = true;
    }

    /// <summary>
    /// Called by the animator to hide the object when it is done opening
    /// </summary>
    public void SetObjectInactive()
    {
        gameObject.SetActive(false);
    }
}
