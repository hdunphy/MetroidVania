using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class IsNearEdgeNode : ActionNode
{
    private Transform CheckTransform;
    private LayerMask GroundLayer;
    private bool UseGroundCheck;

    public float WallRadiusCheck = 0.2f;
    public float GroundDistanceCheck = 2f;

    protected override void OnStart() {
        CheckTransform = context.enemyController.CheckWallTransform;
        GroundLayer = GameLayers.Singleton.GroundLayer;
        UseGroundCheck = blackboard.useGroundCheck;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate()
    {
        Node.State _state;

        var wallCheck = Physics2D.OverlapCircle(CheckTransform.position, WallRadiusCheck, GroundLayer);
        var groundCheck = Physics2D.Raycast(CheckTransform.position, -1 * CheckTransform.up, GroundDistanceCheck, GroundLayer);

        if (wallCheck || (UseGroundCheck && !groundCheck))
        { //if touching a wall or cannot see the floor
            _state = Node.State.Success;
        }
        else
        {
            _state = Node.State.Failure;
        }

        return _state;
    }

    public override void OnDrawGizmos()
    {
        if(CheckTransform != null)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawRay(CheckTransform.position, -1 * CheckTransform.up * GroundDistanceCheck);

            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(CheckTransform.position, WallRadiusCheck);
        }

    }
}
