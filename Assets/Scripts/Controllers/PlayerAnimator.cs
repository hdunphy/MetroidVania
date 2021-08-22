using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerAnimator : MonoBehaviour
{
    /* This class uses Unity Events to set all the animator variables & states */

    [SerializeField] private Animator Animator; //Gets the players Animator

    public void SetIsMoving(bool isMoving)
    {
        Animator.SetBool("IsMoving", isMoving);
    }

    public void SetIsAirborn(bool isAirborn)
    {
        Animator.SetBool("IsAirborn", isAirborn);
    }

    public void SetIsDead(bool isDead)
    {
        Animator.SetBool("IsDead", isDead);
    }

    /// <summary>
    /// Used generically to start the Animator Trigger parameters
    ///     Jump Trigger
    ///     Land Trigger
    ///     Fire Trigger
    ///     Damage Trigger
    /// </summary>
    /// <param name="triggerName"></param>
    public void SetTrigger(string triggerName)
    {
        Animator.SetTrigger(triggerName);
    }

    public void VerticleMovement(CallbackContext callback)
    {
        float verticleMovement = callback.ReadValue<Vector2>().y;
        Animator.SetFloat("Look_Y_Direction", verticleMovement);
    }
}
