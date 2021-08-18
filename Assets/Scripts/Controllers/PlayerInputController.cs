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
        //Debug.Log($"phase: {callback.phase}, started: {callback.started}\nperformed: {callback.performed}, canceled: {callback.canceled}");
        //Controller.TriggerAbility(AbilityEnum.DoubleJump, callback.performed);
        //Controller.TriggerAbility(AbilityEnum.Jump, callback.performed);

        if (callback.started)
        {
            Controller.TriggerAbility(AbilityEnum.DoubleJump, true);
            Controller.TriggerAbility(AbilityEnum.Jump, true);
        }
        else if (callback.canceled)
        {
            Controller.TriggerAbility(AbilityEnum.DoubleJump, false);
            Controller.TriggerAbility(AbilityEnum.Jump, false);
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
