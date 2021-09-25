using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CalculatePlayerPositionNode : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        Vector2 distanceAndDirection = blackboard.player.transform.position - context.enemyController.transform.position;
        blackboard.playerDistance = distanceAndDirection;
        blackboard.moveDirection = new Vector2(distanceAndDirection.x, distanceAndDirection.y);

        return State.Success;
    }
}
