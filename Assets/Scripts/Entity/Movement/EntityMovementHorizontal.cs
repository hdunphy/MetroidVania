using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovementHorizontal : EntityMovementBase
{

    [SerializeField, Tooltip("Absolute value for maximum fall speed")]
    protected float FallSpeedLimit = 25f;

    [SerializeField, Tooltip("Triggers when the entity Jumps")]
    private UnityEvent OnJumpEvent; //When entity jumps

    [SerializeField, Tooltip("Triggers when the entity Dashes")]
    private UnityEvent<bool> OnDashEvent; //When entity Dashes

    [SerializeField, Tooltip("Triggers when the Y velocity of entity changes")]
    private UnityEvent<float> OnYVelocityChange;


    //Internal Values
    private Rigidbody2D m_RigidBody2D; //Entity's rigid body 2d
    private float HorizontalMove = 0f; //current horizontal movement
    private Vector3 Velocity; //referenced velocity used in dampening function
    private bool IsDashing; //Is the entity in the middle of a dash
    private float DashVelocity; //Speed of the dash
    private float LastYVelocity; //Store velocity from last frame

    //Unity properties
    private void Start()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        CanMove = true;
        IsFacingRight = true;
        IsMoving = false;
        IsDashing = false;
        DashVelocity = 0;
        LastYVelocity = 0;
    }

    private void Update()
    {
        CheckIfMoving(new Vector2(HorizontalMove, 0f));

        //Check the yVelocity of the rigid body and normalize it to 1, 0, -1
        float yVelocity = m_RigidBody2D.velocity.y;
        if (yVelocity > 0.01) yVelocity = 1;
        else if (yVelocity < -0.01) yVelocity = -1;
        else yVelocity = 0;

        //if the yVelocity has changed than update the event
        if (yVelocity != LastYVelocity)
        {
            OnYVelocityChange?.Invoke(yVelocity);
        }
        LastYVelocity = yVelocity;
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

            //Check if we need to flip directions
            CheckForFlip(HorizontalMove);
        }

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
            OnDashEvent?.Invoke(true);
        }
    }

    /// <summary>
    /// Stop the dash. Set can move to true and isDashing to false
    /// </summary>
    public void StopDash()
    {
        CanMove = true;
        IsDashing = false;
        OnDashEvent?.Invoke(false);
    }

    //Setters
    public override void SetMoveDirection(Vector2 moveDirection)
    { //Take the sign of the x direction so move speed is always 1, 0, -1
        float xDirection = moveDirection.x == 0 ? 0 : Mathf.Sign(moveDirection.x);
        HorizontalMove =  xDirection * (MovementSpeed * SpeedModifier);
    }
}
