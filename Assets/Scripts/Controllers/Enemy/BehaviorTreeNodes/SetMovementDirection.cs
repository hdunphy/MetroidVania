using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMovementDirection : ActionNode
{
    EntityMovement movement;

    protected override void OnStart()
    {
        movement = blackboard.GetParameter("EntityMovement") as EntityMovement;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        float? movementDirection = blackboard.GetParameter("MovementDirection") as float?;
        movement.SetMoveDirection(movementDirection.Value);

        return NodeState.SUCCESS;
    }
}
