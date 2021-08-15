using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : ICharacterState
{
    private AbilityController AbilityController;
    private EntityMovement Movement;

    public GroundedState(AbilityController abilityController, EntityMovement movement)
    {
        AbilityController = abilityController;
        Movement = movement;
    }

    public void EnterState()
    {
        Movement.SetCanMove(true);
        AbilityController.SetAbilityHasUse(AbilityEnum.Jump, true);
        AbilityController.SetAbilityHasUse(AbilityEnum.DoubleJump, false);
        AbilityController.SetAbilityHasUse(AbilityEnum.Dash, true);
    }

    public void Update()
    {
        AbilityController.SetAbilityHasUse(AbilityEnum.Dash, true);
    }
}
