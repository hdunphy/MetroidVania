using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_Patrol : MonoBehaviour
{
    //[SerializeField] private EntityMovement Movement;
    //[SerializeField, Tooltip("Transform from where to start the physics checks")] private Transform CheckTransform;
    
    //private float MoveDirection; //Keep track of which direction the entity should be moving in

    //private LayerMask GroundLayer; //What is considered ground
    //private float GroundDistanceCheck = 1f; //How far down does the entity need to check for a ground object
    //private const float PhsysicsRadiusCheck = 0.2f; //Radius of physics check for adjacent wall

    //private RootNode root; //Behavior tree Root

    //private void Start()
    //{
    //    GroundLayer = GameLayers.Singleton.GroundLayer; //Get ground layer
    //    MoveDirection = 1;  //initialize to moving right

    //    GenerateTree(); //Create the tree
    //}

    //private void FixedUpdate()
    //{
    //    root.Evaluate(); //Check behavior tree on each fixed update
    //}

    ///// <summary>
    ///// Create the tree behavior from Node objects
    ///// </summary>
    //public void GenerateTree()
    //{
    //    ActionNode FlipNode = new ActionNode(FlipAction); //Flip the movement direction when evaluated
    //    ActionNode MoveNode = new ActionNode(MoveAction); //Update the Movement direction when evalutated

    //    ConditionalNode ChangeDirectionNode = new ConditionalNode(FlipNode, MoveNode, CheckShouldFlip); //Second layer to hold final leaf nodes

    //    root = new RootNode(ChangeDirectionNode); //First layer to hold conditional node
    //}

    ///// <summary>
    ///// Delegate to run when the conditional gets evaluated.
    /////     Checks if the entity is near a wall or on the edge.
    ///// </summary>
    ///// <returns>SUCCESS if near a wall or cannot see the floor. FAILURE if can see the floor and not near a wall</returns>
    //private NodeState CheckShouldFlip()
    //{
    //    NodeState state;

    //    var wallCheck = Physics2D.OverlapCircle(CheckTransform.position, PhsysicsRadiusCheck, GroundLayer);
    //    var FloorCheck = Physics2D.Raycast(CheckTransform.position, Vector2.down, GroundDistanceCheck, GroundLayer);

    //    if(wallCheck || !FloorCheck)
    //    {
    //        state = NodeState.SUCCESS;
    //    }
    //    else
    //    {
    //        state = NodeState.FAILURE;
    //    }

    //    return state;
    //}

    ///// <summary>
    ///// Move in move direction
    ///// </summary>
    ///// <returns>always SUCCESS</returns>
    //private NodeState MoveAction()
    //{
    //    Movement.SetMoveDirection(MoveDirection);

    //    return NodeState.SUCCESS;
    //}

    ///// <summary>
    ///// Flip the movedirection and move in the opposite direction
    ///// </summary>
    ///// <returns>always SUCCESS</returns>
    //private NodeState FlipAction()
    //{
    //    MoveDirection *= -1;
    //    Movement.SetMoveDirection(MoveDirection);

    //    return NodeState.SUCCESS;
    //}
}
