using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalNode : Node
{
    /* Method signature for the action. */
    public delegate NodeStates ConditionalNodeDelegate();

    /* The delegate that is called to evaluate this node */
    private ConditionalNodeDelegate m_conditional;

    private Node m_SuccessNode;
    private Node m_FailNode;

    public ConditionalNode(Node successNode, Node failNode, ConditionalNodeDelegate conditional)
    {
        m_SuccessNode = successNode;
        m_FailNode = failNode;
        m_conditional = conditional;
    }

    public override NodeStates Evaluate()
    {
        NodeStates state;

        switch (m_conditional())
        {
            case NodeStates.FAILURE:
                state = m_FailNode.Evaluate();
                break;
            case NodeStates.SUCCESS:
                state = m_SuccessNode.Evaluate();
                break;
            case NodeStates.RUNNING:
                state = NodeStates.RUNNING;
                break;
            default:
                state = NodeStates.RUNNING;
                break;
        }

        return state;
    }
}
