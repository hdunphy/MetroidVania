using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : IEnemyBehaviour
{
    private EntityMovement Movement;
    private Transform Bounds;

    private bool isInState;
    private float MoveDirection = 1;
    private LayerMask GroundLayer;

    private float GroundDistanceCheck = 1f;
    private const float PhsysicsRadiusCheck = 0.2f;

    private void Start()
    {
        GroundLayer = GameLayers.Singleton.GroundLayer;

        Movement = GetComponent<EntityMovement>();
        Bounds = transform.Find("WallCheck");

        isInState = false;
    }

    private void FixedUpdate()
    {
        if (isInState)
        {
            CheckForFlip();
        }
    }

    private void CheckForFlip()
    {
        //Check if collides with wall or next step is a drop than flip around
        var wallCheck = Physics2D.OverlapCircle(Bounds.position, PhsysicsRadiusCheck, GroundLayer);
        var FloorCheck = Physics2D.Raycast(Bounds.position, Vector2.down, GroundDistanceCheck, GroundLayer);

        //Debug.DrawRay(Bounds.position, Vector2.down, Color.white);

        if (wallCheck || !FloorCheck)
        {
            MoveDirection *= -1;
            Movement.SetMoveDirection(MoveDirection);
        }
    }

    public override void EnterState()
    {
        Movement.SetMoveDirection(MoveDirection);
        isInState = true;
    }

    public override void ExitState()
    {
        Movement.SetMoveDirection(0);
        isInState = false;
    }
}
