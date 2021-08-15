using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornState : ICharacterState
{
    private AbilityController AbilityController;
    private bool AirbornMovement;
    private EntityMovement Movement;

    //Can Attack
    //If double jump is true -> can jump again
    //if can dash is true -> can dash
    //if airborn is true -> can move
    public AirbornState(AbilityController abilityController, bool airbornMovement, EntityMovement movement)
    {
        AbilityController = abilityController;
        AirbornMovement = airbornMovement;
        Movement = movement;
    }

    public void EnterState()
    {
        if (!AirbornMovement)
        {
            Movement.SetCanMove(false);
        }

        AbilityController.SetAbilityHasUse(AbilityEnum.Jump, false);
        AbilityController.SetAbilityHasUse(AbilityEnum.DoubleJump, true);
    }

    void ICharacterState.Update()
    {

    }
}
