using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : MonoBehaviour
{
    /*Component to Take Player Input Events and redirect them to the appropriate methods */

    [SerializeField] private CharacterController2D Controller; //Character controller to apply inputs to

    public void OnMove(CallbackContext callback)
    {
        Controller.SetMove(callback.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext callback)
    {
        
        if (callback.started)
        { //On button pressed, set jump to true
            Controller.TriggerAbility(AbilityEnum.DoubleJump, true);
            Controller.TriggerAbility(AbilityEnum.Jump, true);
        } 
        else if (callback.canceled)
        { //on button released set jump to false
            Controller.TriggerAbility(AbilityEnum.DoubleJump, false);
            Controller.TriggerAbility(AbilityEnum.Jump, false);

            //cancel jump to limit jump height
            Controller.CancelAbility(AbilityEnum.DoubleJump);
            Controller.CancelAbility(AbilityEnum.Jump);
        }
    }

    public void OnDash(CallbackContext callback)
    {
        Controller.TriggerAbility(AbilityEnum.Dash, callback.performed);
    }

    public void OnFire(CallbackContext callback)
    {
        //Controller.SetIsFiring(callback.performed);
    }

    public void OnAttack(CallbackContext callback)
    {
        //Controller.SetIsAttacking(callback.performed);
    }
}
