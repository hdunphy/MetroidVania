using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController
{
    private Dictionary<AbilityEnum, AbilityHolder> Abilities; //Keep track of all character  abilities

    public AbilityController()
    {
        Abilities = new Dictionary<AbilityEnum, AbilityHolder>();
    }

    public AbilityController(List<Ability> startingAbilities, GameObject parentObject) : this()
    {
        foreach(Ability _ability in startingAbilities)
        {
            Abilities.Add(_ability.AbilityType, new AbilityHolder(_ability, parentObject));
        }
    }

    public void Update(float deltaTime)
    {
        foreach (AbilityHolder holder in Abilities.Values)
        { //Update each ability holder using delta time to increment any timers
            holder.Update(deltaTime);
        }
    }

    public void SetAbilityHasUse(AbilityEnum abilityType, bool hasUse)
    {
        if (Abilities.TryGetValue(abilityType, out AbilityHolder holder))
        {
            holder.SetAbilityHasUse(hasUse);
        }
    }

    public void TriggerAbility(AbilityEnum abilityType, bool isButtonPressed)
    {
        //Used to trigger an ability in Ability Dictionary if it exists.
        if (Abilities.TryGetValue(abilityType, out AbilityHolder holder))
        {
            holder.SetAbilityButtonPressed(isButtonPressed);
        }
    }

    public void AddAbility(Ability _ability, GameObject parentGameObject)
    {
        if (Abilities.ContainsKey(_ability.AbilityType))
        {
            //Replace ability?
        }
        else
        {
            Abilities.Add(_ability.AbilityType, new AbilityHolder(_ability, parentGameObject));
        }
    }
}
