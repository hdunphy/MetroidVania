using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public enum DirectionEnum { X, Y}

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
        float playerDistanceAxis = direction == DirectionEnum.X ? blackboard.playerDistance.x : blackboard.playerDistance.y;
        bool isInAttackRange = Mathf.Abs(playerDistanceAxis) <= attackRange;

        return isInAttackRange ? State.Success : State.Failure;
    }
}
