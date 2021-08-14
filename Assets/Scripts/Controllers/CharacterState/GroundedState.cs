using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : ICharacterState
{
    private Dictionary<AbilityEnum, AbilityHolder> Abilities;
    private EntityMovement Movement;

    public GroundedState(Dictionary<AbilityEnum, AbilityHolder> abilities, EntityMovement movement)
    {
        Abilities = abilities;
        Movement = movement;
    }

    public void EnterState()
    {
        Movement.SetCanMove(true);
        if (Abilities.TryGetValue(AbilityEnum.Jump, out AbilityHolder jumpHolder))
        {
            jumpHolder.SetAbilityHasUse(true);
        }

        if (Abilities.TryGetValue(AbilityEnum.DoubleJump, out AbilityHolder doubleJumpHolder))
        {
            doubleJumpHolder.SetAbilityHasUse(false);
        }

        if (Abilities.TryGetValue(AbilityEnum.Dash, out AbilityHolder dashHolder))
        {
            dashHolder.SetAbilityHasUse(true);
        }
    }

    public void Update()
    {
        if (Abilities.TryGetValue(AbilityEnum.Dash, out AbilityHolder dashHolder))
        {
            dashHolder.SetAbilityHasUse(true);
        }
    }
}
