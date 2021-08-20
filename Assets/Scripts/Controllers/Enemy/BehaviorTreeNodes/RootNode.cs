using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootNode : Node
{
    private Node m_Node; //Node to run

    /// <summary>
    /// The root node which starts the evaluation on all the other nodes
    /// </summary>
    /// <param name="node">Node to start of the Behavior tree</param>
    public RootNode(Node node)
    {
        m_Node = node;
    }

    public override NodeStates Evaluate()
    {
        return m_Node.Evaluate();
    }
}
