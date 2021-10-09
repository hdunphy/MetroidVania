using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public enum DirectionEnum { X, Y, XY }

public class IsPlayerInAttackRangeNode : ActionNode
{
    public float attackRange;
    public DirectionEnum direction;

    protected override void OnStart()
    {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        float playerDistance = attackRange + 1;
        switch (direction)
        {
            case DirectionEnum.X:
                playerDistance = blackboard.playerDistance.x;
                break;
            case DirectionEnum.Y:
                playerDistance = blackboard.playerDistance.y;
                break;
            case DirectionEnum.XY:
                playerDistance = blackboard.playerDistance.magnitude;
                break;
        }

        bool isInAttackRange = Mathf.Abs(playerDistance) <= attackRange;
        return isInAttackRange ? State.Success : State.Failure;
    }
}
