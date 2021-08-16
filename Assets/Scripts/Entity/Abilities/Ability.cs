using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityEnum { DoubleJump, Dash, Attack, Shoot, Jump }


public abstract class Ability : ScriptableObject
{ //Scriptable objects are data container files that stay peristent during run time.

    [SerializeField] private new string name; //ability name
    [SerializeField] private AbilityEnum abilityType; //ability type
    [SerializeField, Tooltip("How long it takes for the ability to complete")] private float actionTime; 
    [SerializeField, Tooltip("How long before you can use the ability again")] private float cooldownTime;

    public bool HasUse { get; set; } //Keeps track if this ability is enable/disabled. True if can be used
    public float ActionTime { get => actionTime; } //public member to get the action time
    public float CooldownTime { get => cooldownTime; } //public memeber to get the cooldown time
    public AbilityEnum AbilityType { get => abilityType; } //public member to get the ability type

    /// <summary>
    /// Abstract function called on ability activation
    /// </summary>
    /// <param name="parent">the game object that this ability is connected to</param>
    public abstract void Activate(GameObject parent);

    /// <summary>
    /// Once ability is done and the cooldown timer starts, this function gets called
    /// </summary>
    /// <param name="parent">the game object that this ability is connected to</param>
    public abstract void BeginCooldown(GameObject parent);
}
