using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class SetCanMoveNode : ActionNode
{
    public bool canMove;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        context.entityMovement.SetCanMove(canMove);
        return State.Success;
    }
}
