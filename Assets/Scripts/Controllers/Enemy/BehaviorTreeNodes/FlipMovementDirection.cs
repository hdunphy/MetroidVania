using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipMovementDirection : ActionNode
{
    private float? movementDirection;

    protected override void OnStart()
    {
        movementDirection = blackboard.GetParameter("MovementDirection") as float?;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        movementDirection = movementDirection.Value * -1;
        //movement.SetMoveDirection(movementDirection.Value);
        blackboard.SetParameter("MovementDirection", movementDirection.Value);

        return NodeState.SUCCESS;
    }
}
