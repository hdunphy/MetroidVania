using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    public void Open()
    {
        //Animate
        Animator.enabled = true;
        //Animator.SetTrigger("OpenDoor");
        //gameObject.SetActive(false);
    }

    public void SetObjectInactive()
    {
        gameObject.SetActive(false);
    }
}
