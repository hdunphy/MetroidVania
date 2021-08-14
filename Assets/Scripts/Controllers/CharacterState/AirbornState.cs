using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornState : ICharacterState
{
    private Dictionary<AbilityEnum, AbilityHolder> Abilities;
    private bool AirbornMovement;
    private EntityMovement Movement;

    //Can Attack
    //If double jump is true -> can jump again
    //if can dash is true -> can dash
    //if airborn is true -> can move
    public AirbornState(Dictionary<AbilityEnum, AbilityHolder> abilities, bool airbornMovement, EntityMovement movement)
    {
        Abilities = abilities;
        AirbornMovement = airbornMovement;
        Movement = movement;
    }

    public void EnterState()
    {
        if (!AirbornMovement)
        {
            Movement.SetCanMove(false);
        }

        if (Abilities.TryGetValue(AbilityEnum.Jump, out AbilityHolder jumpHolder))
        {
            jumpHolder.SetAbilityHasUse(false);
        }

        if (Abilities.TryGetValue(AbilityEnum.DoubleJump, out AbilityHolder doubleJumpHolder))
        {
            doubleJumpHolder.SetAbilityHasUse(true);
        }
    }

    void ICharacterState.Update()
    {

    }
}
