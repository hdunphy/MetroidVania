﻿using UnityEngine;

public enum AbilityState { Ready, Active, Cooldown }

public class AbilityHolder
{
    private readonly Ability Ability;  //Ability to trigger
    private readonly GameObject Parent; //Parent game object that uses this ability
    private AbilityState CurrentState; //Current state of the ability
    //private bool IsButtonPressed; //Check if the ability should be triggered on next available update
    private float ActionTime; //Current action time duration
    private float CooldownTime; //Current cooldown time duration

    /// <summary>
    /// Contructor for an ability holder that allows monobehaviors to call this ability
    ///     Handles all the cooldown and enable/disabling
    /// </summary>
    /// <param name="_ability">Ability to be contained by this object</param>
    /// <param name="parent">Parent game object that uses this ability</param>
    public AbilityHolder(Ability _ability, GameObject parent)
    {
        Ability = ScriptableObject.Instantiate(_ability); //Instantiate so that we create a clone of the scriptable object
                                                          //and don't have to worry about multiple instances
        CurrentState = AbilityState.Ready; //Set ability to ready
        Parent = parent;
        Ability.IsButtonPressed = false; //initialize to false so to not immediately trigger the ability
    }

    /// <summary>
    /// Called on every Update() of parent game object
    /// </summary>
    /// <param name="deltaTime">time passed between last frame</param>
    public void Update(float deltaTime)
    {
        //Different behavior between each state
        switch (CurrentState)
        {
            case AbilityState.Ready:
                if (Ability.IsButtonPressed && Ability.HasUse) //If the ability button is being pressed and the ability can be used
                {
                    Ability.Activate(Parent);
                    CurrentState = AbilityState.Active; //Move to the next state
                    ActionTime = Ability.ActionTime; //Start action time counter
                }
                break;
            case AbilityState.Active:
                if (ActionTime > 0)
                { //If actiontime is greater than zero, ability is still activated
                    ActionTime -= deltaTime;
                }
                else
                { //Else ability is done activating and can move into next state
                    Ability.BeginCooldown(Parent);
                    CurrentState = AbilityState.Cooldown; //Move to next state
                    CooldownTime = Ability.CooldownTime; //Start cooldown timer
                }
                break;
            case AbilityState.Cooldown:
                if (CooldownTime > 0)
                { //If CooldownTime is greater than zero, ability is still on cooldown
                    CooldownTime -= deltaTime;
                }
                else
                { //else ability is no longer on cooldown move back to Ready state
                    CurrentState = AbilityState.Ready;
                }
                break;
        }
    }

    /// <summary>
    /// Cancel ability early
    /// </summary>
    public void CancelAbility()
    {
        ActionTime = 0;
        Ability.CancelAbility(Parent);
    }

    /// <summary>
    /// Gets the ability Id that makes each scriptable object unique
    /// </summary>
    /// <returns>String with ability id</returns>
    public string GetAbilityId()
    {
        return Ability.Id;
    }

    //Setters
    public void SetAbilityButtonPressed(bool _isPressed) { Ability.IsButtonPressed = _isPressed; }

    public void SetAbilityHasUse(bool _hasUse) { Ability.HasUse = _hasUse; }
}

