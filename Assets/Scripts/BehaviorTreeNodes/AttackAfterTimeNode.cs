using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class AttackAfterTimeNode : ActionNode
{
    public float duration = 1;
    float startTime;

    protected override void OnStart()
    {
        startTime = Time.time;
        context.entityMovement.SetCanMove(false);
    }

    protected override void OnStop()
    {
        context.enemyController.Attack();
        context.entityMovement.SetCanMove(true);
    }

    protected override State OnUpdate()
    {
        if (Time.time - startTime > duration)
        {
            return State.Success;
        }
        return State.Running;
    }
}
