using UnityEngine;
using System.Collections;

public enum NodeState { RUNNING, SUCCESS, FAILURE }

public abstract class Node : ScriptableObject
{
    public NodeState state = NodeState.RUNNING;
    [HideInInspector] public bool started = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    [HideInInspector] public Blackboard blackboard;
    [HideInInspector] public EnemyController enemyController;
    [TextArea] public string description;

    public NodeState Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();

        if(state == NodeState.FAILURE || state == NodeState.SUCCESS)
        {
            OnStop();
            started = false;
        }

        return state;
    }

    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    public abstract string GetClass();
    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract NodeState OnUpdate();
}