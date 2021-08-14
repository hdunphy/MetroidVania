using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private CharacterController2D Controller;

    public void OnMove(CallbackContext callback)
    {
        Controller.SetMove(callback.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext callback)
    {
        Controller.TriggerAbility(AbilityEnum.DoubleJump, callback.performed);
        Controller.TriggerAbility(AbilityEnum.Jump, callback.performed);
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
