using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController
{
    private Dictionary<AbilityEnum, AbilityHolder> Abilities; //Keep track of all character  abilities

    /// <summary>
    /// Contructor for ability Dictionary with no starting abilities
    /// </summary>
    public AbilityController()
    {
        Abilities = new Dictionary<AbilityEnum, AbilityHolder>();
    }

    /// <summary>
    /// Contructor for Ability Controller with starting abilities
    /// </summary>
    /// <param name="startingAbilities">List of starting abilities to add to the dictionary</param>
    /// <param name="parentObject">game obejct that the abilities are attributed to</param>
    public AbilityController(List<Ability> startingAbilities, GameObject parentObject) : this()
    {
        foreach(Ability _ability in startingAbilities)
        {
            Abilities.Add(_ability.AbilityType, new AbilityHolder(_ability, parentObject));
        }
    }

    /// <summary>
    /// Called every Update() from a monobehavior
    /// </summary>
    /// <param name="deltaTime">The time between frames</param>
    public void Update(float deltaTime)
    {
        foreach (AbilityHolder holder in Abilities.Values)
        { //Update each ability holder using delta time to increment any timers
            holder.Update(deltaTime);
        }
    }

    /// <summary>
    /// Enable/Disable an ability if it exists in the dictionary
    /// </summary>
    /// <param name="abilityType">Ability type to enable or disable </param>
    /// <param name="hasUse"> Bool if the ability is enabled or disabled </param>
    public void SetAbilityHasUse(AbilityEnum abilityType, bool hasUse)
    {
        if (Abilities.TryGetValue(abilityType, out AbilityHolder holder))
        { //Check that the ability exists in the dictionary
            holder.SetAbilityHasUse(hasUse);
        }
    }

    /// <summary>
    /// Used to trigger an ability in Ability Dictionary if it exists.
    /// </summary>
    /// <param name="abilityType">The type of ability needed to trigger</param>
    /// <param name="isButtonPressed">true if ability should be triggered when available, false if stop trying to trigger the ability </param>
    public void TriggerAbility(AbilityEnum abilityType, bool isButtonPressed)
    {
        if (Abilities.TryGetValue(abilityType, out AbilityHolder holder))
        { //Check that the ability exists in the dictionary
            holder.SetAbilityButtonPressed(isButtonPressed);
        }
    }

    /// <summary>
    /// Add an ability to the Ability dictionary
    /// </summary>
    /// <param name="_ability">Ability to be added </param>
    /// <param name="parentGameObject">The parent game object that holds this ability </param>
    public void AddAbility(Ability _ability, GameObject parentGameObject)
    {
        if (Abilities.ContainsKey(_ability.AbilityType))
        { //If the ability already exists in the dictionary
            //Replace ability?
        }
        else
        {
            Abilities.Add(_ability.AbilityType, new AbilityHolder(_ability, parentGameObject));
        }
    }

    /// <summary>
    /// Cancel ability
    /// </summary>
    /// <param name="abilityType">Which ability to cancel</param>
    public void CancelAbility(AbilityEnum abilityType)
    {
        if(Abilities.TryGetValue(abilityType, out AbilityHolder holder))
        {
            holder.CancelAbility();
        }
    }
}
