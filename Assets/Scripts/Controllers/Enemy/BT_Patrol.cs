using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Patrol : MonoBehaviour
{
    [SerializeField] private EntityMovement Movement;
    [SerializeField] private Transform CheckTransform;
    
    private float MoveDirection;

    private LayerMask GroundLayer;
    private float GroundDistanceCheck = 1f;
    private const float PhsysicsRadiusCheck = 0.2f;

    private RootNode root;

    private void Start()
    {
        GroundLayer = GameLayers.Singleton.GroundLayer;
        MoveDirection = 1;  

        GenerateTree();
    }

    private void Update()
    {
        root.Evaluate();
    }

    public void GenerateTree()
    {
        ActionNode FlipNode = new ActionNode(FlipAction);
        ActionNode MoveNode = new ActionNode(MoveAction);

        ConditionalNode ChangeDirectionNode = new ConditionalNode(FlipNode, MoveNode, CheckShouldFlip);

        root = new RootNode(ChangeDirectionNode);
    }

    private NodeStates CheckShouldFlip()
    {
        NodeStates state;

        var wallCheck = Physics2D.OverlapCircle(CheckTransform.position, PhsysicsRadiusCheck, GroundLayer);
        var FloorCheck = Physics2D.Raycast(CheckTransform.position, Vector2.down, GroundDistanceCheck, GroundLayer);

        if(wallCheck || !FloorCheck)
        {
            state = NodeStates.SUCCESS;
        }
        else
        {
            state = NodeStates.FAILURE;
        }

        return state;
    }

    private NodeStates MoveAction()
    {
        Movement.SetMoveDirection(MoveDirection);

        return NodeStates.SUCCESS;
    }

    private NodeStates FlipAction()
    {
        MoveDirection *= -1;
        Movement.SetMoveDirection(MoveDirection);

        return NodeStates.SUCCESS;
    }
}
