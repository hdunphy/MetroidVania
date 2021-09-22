using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SetEntityMovement : ActionNode
{
    public bool SetSpeedModifier;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.entityMovement.SetSpeedModifier(SetSpeedModifier ? context.enemyController.SpeedModifier : 1);
        context.entityMovement.SetMoveDirection(blackboard.moveDirection);
        return State.Success;
    }
}
