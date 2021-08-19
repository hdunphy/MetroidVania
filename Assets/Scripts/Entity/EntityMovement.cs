using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    //Inspector properties
    [SerializeField] private float RunSpeed = 10f; //Velocity applied to horizontal movmenet

    [SerializeField, Tooltip("Absolute value for maximum fall speed")]
    private float FallSpeedLimit = 25f;

    [SerializeField, Tooltip("How much to smooth out the movement")]
    private float MovementSmoothing = 0.05f;


    //Internal Values
    private Rigidbody2D m_RigidBody2D; //Entity's rigid body 2d
    private float HorizontalMove = 0f; //current horizontal movement
    private bool CanMove; //enable/disable movement
    private bool IsFacingRight; //Is entity looking to the right
    private Vector3 Velocity; //referenced velocity used in dampening function

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
            { //If entity is moving in negative horizontal direction (or left) but is facing right
                Flip();
            }
            else if (HorizontalMove > 0 && !IsFacingRight)
            { //If entity is moving in positive horizontal direction (or right) but is facing left
                Flip();
            }
        }

    }

    /// <summary>
    /// Flip the direction of the entity
    /// </summary>
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        IsFacingRight = !IsFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /// <summary>
    /// If the entity can Move trigger the jump ability
    /// </summary>
    /// <param name="jumpVelocity">Set the Y velocity of the entity to this value</param>
    public void TriggerJump(float jumpVelocity)
    {
        if (CanMove)
        {
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, jumpVelocity);
        }
    }

    /// <summary>
    /// Set Y velocity to 0 so player stops rising
    /// </summary>
    public void EndJump()
    {
        if (CanMove && m_RigidBody2D.velocity.y > 0)
        {
            m_RigidBody2D.velocity = new Vector2(m_RigidBody2D.velocity.x, 0);
        }
    }

    /// <summary>
    /// If the entity can move trigger the dash ability
    /// </summary>
    /// <param name="dashForce">Set the velocity of the dash in the x direction</param>
    public void TriggerDash(float dashForce)
    {
        if (CanMove)
        {
            m_RigidBody2D.velocity = new Vector2(transform.localScale.x * dashForce, 0);
        }
    }

    //Setters
    public void SetCanMove(bool _canMove) { CanMove = _canMove; }
    public void SetMoveDirection(float moveX) { HorizontalMove = moveX * RunSpeed; }
}
