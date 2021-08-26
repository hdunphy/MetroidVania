using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequencerNode : CompositeNode
{
    int current;

    protected override void OnStart()
    {
        current = 0;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        var child = children[current];

        switch (child.Update())
        {
            case NodeState.RUNNING:
                return NodeState.RUNNING;
            case NodeState.SUCCESS:
                current++;
                break;
            case NodeState.FAILURE:
                return NodeState.FAILURE;
        }

        return current == children.Count ? NodeState.SUCCESS : NodeState.RUNNING;
    }
}