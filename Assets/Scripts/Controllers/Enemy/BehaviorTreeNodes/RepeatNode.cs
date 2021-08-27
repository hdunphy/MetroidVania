using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatNode : DecoratorNode
{
    public bool isInfinite;
    public int repeatCount;

    private int count;
    protected override void OnStart()
    {
        count = 0;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        NodeState _state = NodeState.RUNNING;
        if (isInfinite)
        {
            child.Update();
        }
        else if(count < repeatCount)
        {
            if (child.Update() == NodeState.FAILURE)
            {
                count++;
            }

            _state = count == repeatCount ? NodeState.SUCCESS : NodeState.RUNNING;
        }



        return _state;
    }
}
