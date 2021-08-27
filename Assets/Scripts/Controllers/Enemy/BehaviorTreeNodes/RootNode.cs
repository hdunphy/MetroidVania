using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    public Node child;

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        return child.Update();
    }

    public override Node Clone()
    {
        RootNode node = Instantiate(this);
        node.child = child.Clone();
        return node;
    }

    public override string GetClass()
    {
        return "root";
    }
}
