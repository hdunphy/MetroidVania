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

    public void SetIsDashing(bool isDashing)
    {
        Animator.SetBool("IsDashing", isDashing);
    }

    /// <summary>
    /// Used generically to start the Animator Trigger parameters
    ///     Land Trigger
    ///     Fire Trigger
    /// </summary>
    /// <param name="triggerName"></param>
    public void SetTrigger(string triggerName)
    {
        Animator.SetTrigger(triggerName);
    }

    /// <summary>
    /// From PlayerInput component, read in the OnMove input and take the y value to determine where the player is looking
    /// </summary>
    /// <param name="callback">Player input</param>
    public void VerticleMovement(CallbackContext callback)
    {
        float verticleMovement = callback.ReadValue<Vector2>().y;
        Animator.SetFloat("Look_Y_Direction", verticleMovement);
    }

    /// <summary>
    /// Get player y velocity and set the float for getting player is jumping or falling
    /// </summary>
    /// <param name="velocity">Velocity of player in y direction</param>
    public void YVelocity(float velocity)
    {
        Animator.SetFloat("Y_Velocity", velocity);
    }
}
