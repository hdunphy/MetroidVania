using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveTowardsPlayerNode : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float distanceAndDirection = (blackboard.player.transform.position - context.enemyController.transform.position).x;
        blackboard.playerDistance = Mathf.Abs(distanceAndDirection);
        blackboard.moveDirection = Mathf.Sign(distanceAndDirection);

        return State.Success;
    }
}
