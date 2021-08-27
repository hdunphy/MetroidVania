using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertorNode : DecoratorNode
{
    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        NodeState _state = NodeState.FAILURE;

        switch (child.Update())
        {
            case NodeState.RUNNING:
                _state = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                _state = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _state = NodeState.SUCCESS;
                break;
        }

        return _state;
    }
}
