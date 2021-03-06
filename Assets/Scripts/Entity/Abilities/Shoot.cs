using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shoot ability", menuName = "Scriptable/Abilities/Shoot")]
public class Shoot : Ability
{
    [SerializeField, Tooltip("Prefab to be instatiated when ability is triggerd")] private ProjectileController ProjectilePrefab;
    [SerializeField] private float ProjectileSpeed;

    public override void Activate(GameObject parent)
    {
        if(parent.TryGetComponent(out ShootingController controller))
        {
            controller.TriggerOnShootInit(ProjectilePrefab, ProjectileSpeed);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
            
    }

    public override void CancelAbility(GameObject parent)
    {

    }
}
