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
        if(blackboard.moveToPosition != Vector3.zero)
            PathFinding.UpdatePath(blackboard.moveToPosition);
    }

    protected override void OnStop()
    {

    }

    protected override State OnUpdate()
    {
        State _state;
        Vector2 _direction = Vector2.zero;

        if (blackboard.moveToPosition == Vector3.zero)
        {
            _state = State.Failure;
        }
        else if(PathFinding.StepsLeftInPath() == 0)
        {
            _state = State.Success;
        }
        else
        {
            _direction = PathFinding.GetDirection();
            _state = _direction == Vector2.zero ? State.Failure : State.Running;
        }

        context.entityMovement.SetMoveDirection(_direction);

        if(_state != State.Running)
        {
            blackboard.moveToPosition = Vector3.zero;
        }

        return _state;
    }
}
