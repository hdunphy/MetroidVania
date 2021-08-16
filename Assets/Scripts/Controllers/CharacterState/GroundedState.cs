using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : ICharacterState
{
    private AbilityController AbilityController;
    private EntityMovement Movement;

    /// <summary>
    /// Grounded State Constructor
    /// </summary>
    /// <param name="abilityController">Ability controller so State can turn on and off abilities</param>
    /// <param name="movement">Objects movement controller</param>
    public GroundedState(AbilityController abilityController, EntityMovement movement)
    {
        AbilityController = abilityController;
        Movement = movement;
    }

    public void EnterState()
    {
        //Character can always move when entering grounded state
        Movement.SetCanMove(true);
        //Enable movement abilities: Jump, Dash
        AbilityController.SetAbilityHasUse(AbilityEnum.Jump, true);
        AbilityController.SetAbilityHasUse(AbilityEnum.Dash, true);

        //Disable Double Jump ability since it uses the same key binding
        AbilityController.SetAbilityHasUse(AbilityEnum.DoubleJump, false);
    }

    public void Update()
    {
        //Need to make sure Dash is always enabled when grounded.S
        AbilityController.SetAbilityHasUse(AbilityEnum.Dash, true);
    }
}
