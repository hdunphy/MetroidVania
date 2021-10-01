using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class MoveToTargetNode : ActionNode
{
    private IPathFinding PathFinding;

    protected override void OnStart()
    {
        PathFinding = context.gameObject.GetComponent<IPathFinding>();
        PathFinding.UpdatePath(blackboard.player.transform.position);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        State _state;

        if(PathFinding.StepsLeftInPath() == 0)
        {
            context.entityMovement.SetMoveDirection(Vector2.zero);
            _state = State.Success;
        }
        else
        {
            Vector2 _direction = PathFinding.GetDirection();
            context.entityMovement.SetMoveDirection(_direction);
            _state = _direction == Vector2.zero ? State.Failure : State.Running;
        }

        return _state;
    }
}
