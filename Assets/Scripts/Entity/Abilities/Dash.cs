using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable/Abilities/Dash")]
public class Dash : Ability
{
    [SerializeField, Tooltip("Velocity of the Dash")] private float DashVelocity;

    public override void Activate(GameObject parent)
    {
        //Trigger dash movement ability
        parent.GetComponent<EntityMovement>().TriggerDash(DashVelocity);
        //Disable ability
        HasUse = false;
    }

    public override void BeginCooldown(GameObject parent)
    {
    }

    public override void CancelAbility(GameObject parent)
    {
    }
}
