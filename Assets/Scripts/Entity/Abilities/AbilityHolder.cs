using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public enum AbilityState { Ready, Active, Cooldown }

public class AbilityHolder //: MonoBehaviour
{
    private Ability Ability;
    private AbilityState CurrentState;
    private GameObject Parent;
    private bool IsButtonPressed;
    private float ActionTime;
    private float CooldownTime;

    public AbilityHolder(Ability _ability, GameObject parent)
    {
        Ability = _ability;
        CurrentState = AbilityState.Ready;
        Parent = parent;
        IsButtonPressed = false;
    }

    public void Update(float deltaTime)
    {
        switch (CurrentState)
        {
            case AbilityState.Ready:
                if (IsButtonPressed && Ability.HasUse)
                {
                    Ability.Activate(Parent);
                    CurrentState = AbilityState.Active;
                    ActionTime = Ability.ActionTime;
                }
                break;
            case AbilityState.Active:
                if(ActionTime > 0)
                {
                    ActionTime -= deltaTime;
                }
                else
                {
                    Ability.BeginCooldown(Parent);
                    CurrentState = AbilityState.Cooldown;
                    CooldownTime = Ability.CooldownTime;
                }
                break;
            case AbilityState.Cooldown:
                if (CooldownTime > 0)
                {
                    CooldownTime -= deltaTime;
                }
                else
                {
                    CurrentState = AbilityState.Ready;
                }
                break;
        }
    }

    public void SetAbilityButtonPressed(bool _isPressed) { IsButtonPressed = _isPressed; }

    public void SetAbilityHasUse(bool _hasUse) { Ability.HasUse = _hasUse; }
}

