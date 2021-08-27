using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitNode : ActionNode
{
    public float Duration;
    private float StartTime;

    protected override void OnStart()
    {
        StartTime = Time.time;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        NodeState _state;
        if(StartTime + Duration < Time.time)
        {
            _state = NodeState.RUNNING;
        }
        else
        {
            _state = NodeState.SUCCESS;
        }

        return _state;
    }
}
