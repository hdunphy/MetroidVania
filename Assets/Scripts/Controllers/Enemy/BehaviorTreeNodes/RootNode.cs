using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    private Node m_Node;

    public RootNode(Node node)
    {
        m_Node = node;
    }

    public override NodeStates Evaluate()
    {
        return m_Node.Evaluate();
    }
}
