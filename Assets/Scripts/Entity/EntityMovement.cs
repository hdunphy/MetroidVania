using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    //Inspector properties
    [SerializeField] private float RunSpeed = 10f; //Velocity applied to horizontal movmenet

    [SerializeField, Tooltip("Absolute value for maximum fall speed")]
    private float FallSpeedLimit = 25f;

    [SerializeField, Tooltip("How much to smooth out the movement")]
    private float MovementSmoothing = 0.05f;

    [SerializeField, Tooltip("If entity changes moving state, trigger this event")]
    private UnityEvent<bool> SetIsMoving; //True if moving, False if idle

    [SerializeField, Tooltip("Triggers when the entity Jumps")]
    private UnityEvent OnJumpEvent; //When player jumps


    //Internal Values
    private Rigidbody2D m_RigidBody2D; //Entity's rigid body 2d
    private float HorizontalMove = 0f; //current horizontal movement
    private bool CanMove; //enable/disable movement
    private bool IsFacingRight; //Is entity looking to the right
    private Vector3 Velocity; //referenced velocity used in dampening function
    private bool IsMoving; //Store moving state from last frame;
    private bool IsDashing; //Is the entity in the middle of a dash
    private float DashVelocity; //Speed of the dash

    //Unity properties
    private void Start()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        CanMove = true;
        IsFacingRight = true;
        IsMoving = false;
        IsDashing = false;
        DashVelocity = 0;
    }

    private void Update()
    {
        bool movedLastFrame = IsMoving; //Save state to see if there has been a change in state since last frame
        IsMoving = Mathf.Abs(HorizontalMove) > 0.01; //Set movement true if has a value in either direction
        if (IsMoving != movedLastFrame)
        { //Invoke the event when a change in state occurs
            SetIsMoving?.Invoke(IsMoving);
        }
    }

    private void FixedUpdate()
    {
        if (IsDashing)
        { //If entity is in the middle of a dash
            m_RigidBody2D.velocity = new Vector2(transform.localScale.x * DashVelocity, 0); //Set y velocity to 0 so that entity won't fall
        }
        else if (CanMove) //Check if entity can move
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
            OnJumpEvent?.Invoke(); //Trigger on jump event for other componets
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
    ///     Turn on IsDashing and turn off can move
    /// </summary>
    /// <param name="_dashVelocity">Set the velocity of the dash in the x direction</param>
    public void TriggerDash(float _dashVelocity)
    {
        if (CanMove)
        {
            IsDashing = true;
            DashVelocity = _dashVelocity;
            CanMove = false;
        }
    }

    /// <summary>
    /// Stop the dash. Set can move to true and isDashing to false
    /// </summary>
    public void StopDash()
    {
        CanMove = true;
        IsDashing = false;
    }

    //Setters
    public void SetCanMove(bool _canMove) { CanMove = _canMove; }
    public void SetMoveDirection(float moveX) { HorizontalMove = moveX * RunSpeed; }
}
