using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundedState : ICharacterState
{
    private AbilityController AbilityController;
    private EntityMovementHorizontal Movement;
    private UnityEvent<bool> IsAriborn;

    /// <summary>
    /// Grounded State Constructor
    /// </summary>
    /// <param name="abilityController">Ability controller so State can turn on and off abilities</param>
    /// <param name="movement">Objects movement controller</param>
    /// <param name="_isAirborn">Unity event triggered when airborn state is changed</param>
    public GroundedState(AbilityController abilityController, EntityMovementHorizontal movement, UnityEvent<bool> _isAirborn)
    {
        AbilityController = abilityController;
        Movement = movement;
        IsAriborn = _isAirborn;
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

        //Set airborn state
        IsAriborn?.Invoke(false);
    }

    public void Update()
    {
        //Need to make sure Dash is always enabled when grounded.S
        AbilityController.SetAbilityHasUse(AbilityEnum.Dash, true);
    }
}
