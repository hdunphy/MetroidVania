using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimatorByEvents : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    public void TriggerEvent(string triggerName)
    {
        Animator.SetTrigger(triggerName);
    }

    public void SetIsMoving(bool isMoving)
    {
        Animator.SetBool("IsMoving", isMoving);
    }
}
