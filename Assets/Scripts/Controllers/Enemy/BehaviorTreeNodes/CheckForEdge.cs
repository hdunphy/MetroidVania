using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForEdge : ActionNode
{
    private Transform CheckTransform;
    private LayerMask GroundLayer;

    public float WallRadiusCheck = 0.2f;
    public float GroundDistanceCheck = 2f;


    protected override void OnStart()
    {
        CheckTransform = blackboard.GetParameter("CheckTransform") as Transform;
        GroundLayer = (blackboard.GetParameter("GroundLayer") as LayerMask?).Value;
    }

    protected override void OnStop()
    {
    }

    protected override NodeState OnUpdate()
    {
        NodeState _state;

        var wallCheck = Physics2D.OverlapCircle(CheckTransform.position, WallRadiusCheck, GroundLayer);
        var floorCheck = Physics2D.Raycast(CheckTransform.position, Vector2.down, GroundDistanceCheck, GroundLayer);

        if(wallCheck || !floorCheck)
        { //if touching a wall or cannot see the floor
            _state = NodeState.SUCCESS;
        }
        else
        {
            _state = NodeState.FAILURE;
        }

        return _state;
    }
}
