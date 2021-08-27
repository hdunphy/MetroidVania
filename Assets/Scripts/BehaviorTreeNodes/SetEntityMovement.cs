using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SetEntityMovement : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.entityMovement.SetMoveDirection(blackboard.moveDirection);
        return State.Success;
    }
}
