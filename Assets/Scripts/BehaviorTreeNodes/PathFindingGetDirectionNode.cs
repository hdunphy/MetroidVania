using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class PathFindingGetDirectionNode : ActionNode
{
    private IPathFinding PathFinding;

    protected override void OnStart() {
        PathFinding = context.gameObject.GetComponent<IPathFinding>();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        PathFinding.UpdatePath(blackboard.player.transform.position);
        blackboard.moveDirection = PathFinding.GetDirection();
        return State.Success;
    }
}
