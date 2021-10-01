using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class BehaviorTreeRunnerNode : ActionNode
{
    [Tooltip("Do not have cyclical references")]
    public BehaviourTree behaviourTree;

    private Node firstChild;

    public override Node Clone()
    {
        //BehaviorTreeRunnerNode node = Instantiate(this);
        //node.behaviourTree = behaviourTree.Clone();
        if (behaviourTree != null)
        {
            var _btRoot = (RootNode)behaviourTree.rootNode;
            firstChild = _btRoot.child.Clone();
            //firstChild.Bind(context, blackboard);
        }
        return firstChild;
    }

    public override void Bind(EnemyContext _context, Blackboard _blackboard)
    {
        base.Bind(_context, _blackboard);
        behaviourTree.Bind(context, _blackboard);
    }

    protected override void OnStart()
    {
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return behaviourTree.Update();
    }
}
