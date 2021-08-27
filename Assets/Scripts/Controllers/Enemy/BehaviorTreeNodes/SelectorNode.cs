using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectorNode : CompositeNode
{
    private int currentIndex;

    protected override void OnStart()
    {
        currentIndex = 0;
    }

    protected override void OnStop()
    {

    }

    protected override NodeState OnUpdate()
    {
        Node child = children[currentIndex];

        switch (child.Update())
        {
            case NodeState.RUNNING:
                return NodeState.RUNNING; //if running keep running same child in next update
            case NodeState.SUCCESS:
                return NodeState.SUCCESS; //if a success then get out of selector
            case NodeState.FAILURE:
                currentIndex++; //continue if there is a failure until selector gets a success
                break;
        }

        //At end of loop, if no successes than return failure. 
        //  Else will only get here when got a failure and still has children
        return currentIndex == children.Count ? NodeState.FAILURE : NodeState.RUNNING;
    }
}