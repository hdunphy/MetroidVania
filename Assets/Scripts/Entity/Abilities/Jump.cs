using UnityEngine;

[CreateAssetMenu(fileName = "Jump", menuName = "Scriptable/Abilities/Jump")]
public class Jump : Ability
{
    [SerializeField] private float JumpForce = 30f;

    public override void Activate(GameObject parent)
    {
        if(parent.TryGetComponent(out EntityMovement movement))
        {
            movement.TriggerJump(JumpForce);
        }

        if(parent.TryGetComponent(out CharacterController2D controller))
        {
            controller.TriggerAbility(AbilityEnum.DoubleJump, false);
        }

        //HasUse = false;
    }

    public override void BeginCooldown(GameObject parent)
    {
    }
}
