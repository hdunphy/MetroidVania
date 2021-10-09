using UnityEngine;

public class EntityMovement2D : EntityMovementBase
{
    private Rigidbody2D m_RigidBody2D;
    private Vector2 MoveDirection;
    private Vector3 Velocity; //referenced velocity used in dampening function

    //Unity properties
    private void Start()
    {
        m_RigidBody2D = GetComponent<Rigidbody2D>();
        CanMove = true;
        IsFacingRight = true;
        IsMoving = false;
    }

    public override void SetMoveDirection(Vector2 moveDirection)
    {
        MoveDirection = moveDirection.normalized;
    }

    private void Update()
    {
        CheckIfMoving(MoveDirection);
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            // Move the character by finding the target velocity
            Vector3 targetVelocity = MoveDirection * MovementSpeed * SpeedModifier;
            // And then smoothing it out and applying it to the character
            m_RigidBody2D.velocity = Vector3.SmoothDamp(m_RigidBody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

            //Check if we need to flip directions
            CheckForFlip(MoveDirection.x);
        }
    }
}
