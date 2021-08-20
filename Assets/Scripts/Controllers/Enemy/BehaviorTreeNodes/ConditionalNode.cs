using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalNode : Node
{
    /* Method signature for the action. */
    public delegate NodeStates ConditionalNodeDelegate();

    /* The delegate that is called to determine which branching node to run */
    private ConditionalNodeDelegate m_conditional;

    private Node m_SuccessNode; //Node to evaluate on Success
    private Node m_FailNode; //Node to evaluate on Fail

    /// <summary>
    /// Conditional Node contains two nodes and a conditional delegate.
    ///     The delegate will determine which node to run
    /// </summary>
    /// <param name="successNode">Node to evaluate on Success</param>
    /// <param name="failNode">Node to evaluate on Fail</param>
    /// <param name="conditional">Function to determine which node to run</param>
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
            case NodeStates.RUNNING: //Optional state if needs to stay in running and not choose a Node yet
                state = NodeStates.RUNNING;
                break;
            default:
                state = NodeStates.RUNNING;
                break;
        }

        return state;
    }
}
