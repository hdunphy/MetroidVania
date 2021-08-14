using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityEnum { DoubleJump, Dash, Attack, Shoot, Jump }

//[CreateAssetMenu(fileName = "Ability", menuName = "Scriptable/Abilities")]
public abstract class Ability : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private AbilityEnum abilityType;
    [SerializeField] private float actionTime;
    [SerializeField] private float cooldownTime;

    public bool HasUse { get; set; }
    public float ActionTime { get => actionTime; }
    public float CooldownTime { get => cooldownTime; }
    public AbilityEnum AbilityType { get => abilityType; }

    public abstract void Activate(GameObject parent);
    public abstract void BeginCooldown(GameObject parent);
}
