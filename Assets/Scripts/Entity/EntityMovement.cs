using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    //Inspector properties
    [SerializeField] private float RunSpeed = 10f;

    [SerializeField, Tooltip("Absolute value for maximum fall speed")]
    private float FallSpeedLimit = 25f;

    [SerializeField, Tooltip("How much to smooth out the movement")]
    private float MovementSmoothing = 0.05f;


    //Internal Values
    private Rigidbody2D m_RigidBody2D;
    private float HorizontalMove = 0f;
    private bool CanMove;
    private bool IsFacingRight;
    private Vector3 Velocity;

    //Unity properties
    private void Start()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        CanMove = true;
        IsFacingRight = true;
    }

    private void FixedUpdate()
    {
        if (CanMove) //Check if entity can move
        {
            if (m_RigidBody2D.velocity.y < -FallSpeedLimit)
            { //Limit the entity's falling speed
                m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, -FallSpeedLimit);
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(HorizontalMove, m_RigidBody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_RigidBody2D.velocity = Vector3.SmoothDamp(m_RigidBody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

            if (HorizontalMove < 0 && IsFacingRight)
            {
                Flip();
            }
            else if (HorizontalMove > 0 && !IsFacingRight)
            {
                Flip();
            }
        }

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        IsFacingRight = !IsFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void TriggerJump(float jumpForce)
    {
        if (CanMove)
        {
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, jumpForce);
        }
    }

    public void TriggerDash(float dashForce)
    {
        if (CanMove)
        {
            m_RigidBody2D.velocity = new Vector2(transform.localScale.x * dashForce, 0);
        }
    }

    public void SetCanMove(bool _canMove) { CanMove = _canMove; }
    public void SetMoveDirection(float moveX) { HorizontalMove = moveX * RunSpeed; }
}
