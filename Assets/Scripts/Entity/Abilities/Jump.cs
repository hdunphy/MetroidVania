using UnityEngine;

[CreateAssetMenu(fileName = "Jump", menuName = "Scriptable/Abilities/Jump")]
public class Jump : Ability
{
    [SerializeField, Tooltip("Y Velocity that gets set when Jumping")] private float JumpVelocity = 30f;

    public override void Activate(GameObject parent)
    {
        if(parent.TryGetComponent(out EntityMovement movement))
        {
            movement.TriggerJump(JumpVelocity);
        }

        if(parent.TryGetComponent(out CharacterController2D controller))
        { //Might be overkill, but don't want to trigger the jump and the double jump at the same time
            controller.TriggerAbility(AbilityEnum.DoubleJump, false);
        }
    }

    public override void BeginCooldown(GameObject parent)
    {
    }
}
