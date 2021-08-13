using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private Attack Attack;
    [SerializeField] private PlayerMovement PlayerMovement;

    private Vector2 inputVector;

    public void OnMove(CallbackContext callback)
    {
        inputVector = callback.ReadValue<Vector2>();
        PlayerMovement.SetMoveDirection(inputVector);
    }

    public void OnJump(CallbackContext callback)
    {
        PlayerMovement.SetIsJumping(callback.performed);
    }

    public void OnDash(CallbackContext callback)
    {
        PlayerMovement.SetIsDashing(callback.performed);
    }

    public void OnFire(CallbackContext callback)
    {
        Attack.SetIsFiring(callback.performed);
    }

    public void OnAttack(CallbackContext callback)
    {
        Attack.SetIsAttacking(callback.performed);
    }
}
