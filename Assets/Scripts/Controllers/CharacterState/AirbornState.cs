using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirbornState : ICharacterState
{
    private AbilityController AbilityController;
    private bool AirbornMovement;
    private EntityMovement Movement;

    /// <summary>
    /// Airborn state constructor
    /// </summary>
    /// <param name="abilityController">Ability controller so State can turn on and off abilities</param>
    /// <param name="airbornMovement">Set if character can move while airborn</param>
    /// <param name="movement">Objects movement controller</param>
    public AirbornState(AbilityController abilityController, bool airbornMovement, EntityMovement movement)
    {
        AbilityController = abilityController;
        AirbornMovement = airbornMovement;
        Movement = movement;
    }

    public void EnterState()
    {
        if (!AirbornMovement)
        { //If this character cannot move in the air then when entering airborn state set can move to false
            Movement.SetCanMove(false);
        }

        //Jump ability must be set to false. Can not jump once is airborn
        AbilityController.SetAbilityHasUse(AbilityEnum.Jump, false);
        //Double jump ability is enabled. Double jump can only happen while airborn
        AbilityController.SetAbilityHasUse(AbilityEnum.DoubleJump, true);
    }

    void ICharacterState.Update()
    {
        //Nothing happens in Update
    }
}
