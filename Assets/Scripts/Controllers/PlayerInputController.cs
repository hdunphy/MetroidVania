using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputController : MonoBehaviour
{
    /*Component to Take Player Input Events and redirect them to the appropriate methods */

    [SerializeField] private CharacterController2D CharacterController; //Character controller to apply inputs to
    [SerializeField] private PlayerController PlayerController;

    public void OnMove(CallbackContext callback)
    {
        CharacterController.SetMove(callback.ReadValue<Vector2>());
    }

    public void OnJump(CallbackContext callback)
    {
        
        if (callback.started)
        { //On button pressed, set jump to true
            CharacterController.TriggerAbility(AbilityEnum.DoubleJump, true);
            CharacterController.TriggerAbility(AbilityEnum.Jump, true);
        } 
        else if (callback.canceled)
        { //on button released set jump to false
            CharacterController.TriggerAbility(AbilityEnum.DoubleJump, false);
            CharacterController.TriggerAbility(AbilityEnum.Jump, false);

            //cancel jump to limit jump height
            CharacterController.CancelAbility(AbilityEnum.DoubleJump);
            CharacterController.CancelAbility(AbilityEnum.Jump);
        }
    }

    public void OnDash(CallbackContext callback)
    {
        CharacterController.TriggerAbility(AbilityEnum.Dash, callback.performed);
    }

    public void OnFire(CallbackContext callback)
    {
        CharacterController.TriggerAbility(AbilityEnum.Shoot, callback.performed);
    }

    public void OnInteraction(CallbackContext callback)
    {
        if (callback.started)
        {
            PlayerController.OnPlayerInteraction();
        }
    }

    public void OnPause(CallbackContext callback)
    {
        if (callback.started)
        {
            if(GameSceneController.Singleton.CurrentGameState == GameSceneController.GameState.InGame)
            {
                GameSceneController.Singleton.SetPaused(true);
            }
            //else if (GameSceneController.Singleton.CurrentGameState == GameSceneController.GameState.Paused)
            //{
            //    GameSceneController.Singleton.SetPaused(false);
            //}
        }
    }

    public void EnableInput(bool isEnabled)
    {
        GetComponent<PlayerInput>().enabled = isEnabled;
    }
}
