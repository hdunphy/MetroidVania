using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAbilityManager : MonoBehaviour
{
    public static PlayerAbilityManager Singleton;

    [SerializeField] private List<AbilityContainer> PlayerAbilities;

    private void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<string> GetStartingAbilities()
    {
        return PlayerAbilities.Where(x => x.IsStartingPlayerAbility).
            Select(s => s.Ability.Id).ToList();
    }

    public List<Ability> GetAbilitiesByIds(List<string> playerHeldAbilityIds)
    {
        return PlayerAbilities.Where(x => playerHeldAbilityIds.Contains(x.Ability.Id)).
            Select(x => x.Ability).ToList();
    }
}

[Serializable]
public struct AbilityContainer
{
    public Ability Ability;
    public bool IsStartingPlayerAbility;
}
