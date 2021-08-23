using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable/Abilities/Dash")]
public class Dash : Ability
{
    [SerializeField, Tooltip("Velocity of the Dash")] private float DashVelocity;

    public override void Activate(GameObject parent)
    {
        EntityMovement movement = parent.GetComponent<EntityMovement>();
        //Trigger dash movement ability
        movement.TriggerDash(DashVelocity);

        //Disable dash
        HasUse = false;
    }

    public override void BeginCooldown(GameObject parent)
    {
        StopDash(parent);
    }

    public override void CancelAbility(GameObject parent)
    {
        StopDash(parent);
    }

    private void StopDash(GameObject parent)
    {
        EntityMovement movement = parent.GetComponent<EntityMovement>();
        movement.StopDash();
    }
}
