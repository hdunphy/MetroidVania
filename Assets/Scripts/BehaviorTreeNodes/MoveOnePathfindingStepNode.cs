using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveOnePathfindingStepNode : ActionNode
{
    public float StuckTimer = 1f;
    private IPathFinding PathFinding;
    private Vector2 StartDirection;
    private float _timer;

    protected override void OnStart()
    {
        PathFinding = context.gameObject.GetComponent<IPathFinding>();
        blackboard.moveToPosition = blackboard.player.transform.position;
        PathFinding.UpdatePath(blackboard.moveToPosition);
        StartDirection = PathFinding.GetDirection();

        _timer = StuckTimer;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        State _state;
        Vector2 _direction = PathFinding.GetDirection();

        if (PathFinding.StepsLeftInPath() == 0 || _direction != StartDirection)
        {
            context.entityMovement.SetMoveDirection(Vector2.zero);
            _state = State.Success;
        }
        else
        {
            context.entityMovement.SetMoveDirection(_direction);
            _state = _direction == Vector2.zero ? State.Failure : State.Running;
        }

        if (_state == State.Running && _timer <= 0)
        {
            _state = State.Failure;
        }
        else
        {
            _timer -= Time.deltaTime;
        }

        return _state;
    }
}
