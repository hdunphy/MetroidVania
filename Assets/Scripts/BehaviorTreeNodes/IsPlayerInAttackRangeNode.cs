using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsPlayerInAttackRangeNode : ActionNode
{
    public float attackRange;

    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        bool isInAttackRange = blackboard.playerDistance <= attackRange;

        return isInAttackRange ? State.Success : State.Failure;
    }
}
