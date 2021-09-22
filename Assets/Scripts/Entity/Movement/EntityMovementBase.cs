using UnityEngine;
using UnityEngine.Events;

public abstract class EntityMovementBase : MonoBehaviour, IEntityMovement
{
    [SerializeField] protected float MovementSpeed = 10f;

    [SerializeField, Tooltip("How much to smooth out the movement")]
    protected float MovementSmoothing = 0.05f;

    [SerializeField, Tooltip("If entity changes moving state, trigger this event")]
    protected UnityEvent<bool> SetIsMoving; //True if moving, False if idle

    protected bool CanMove; //enable/disable movement
    protected bool IsFacingRight; //Is entity looking to the right
    protected float SpeedModifier = 1; //Modifier to multiply speed by. Defaulted to 1
    protected bool IsMoving; //Store moving state from last frame;

    private const float MOVEMENT_CHECK = 0.01f;

    /// <summary>
    /// Flip the direction of the entity
    /// </summary>
    protected void Flip()
    {
        // Switch the way the player is labelled as facing.
        IsFacingRight = !IsFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    /// <summary>
    /// Check if entity needs to flip facing direction
    /// </summary>
    /// <param name="horizontalMove">movement in the x direction</param>
    protected void CheckForFlip(float horizontalMove)
    {
        if (horizontalMove < 0 && IsFacingRight)
        { //If entity is moving in negative horizontal direction (or left) but is facing right
            Flip();
        }
        else if (horizontalMove > 0 && !IsFacingRight)
        { //If entity is moving in positive horizontal direction (or right) but is facing left
            Flip();
        }
    }

    /// <summary>
    /// Check if is moving and if last frame was not moving trigger SetIsMoving event
    /// </summary>
    /// <param name="movement">movement direction of entity</param>
    protected void CheckIfMoving(Vector2 movement)
    {
        bool movedLastFrame = IsMoving; //Save state to see if there has been a change in state since last frame
        IsMoving = movement.sqrMagnitude > (MOVEMENT_CHECK * MOVEMENT_CHECK); //Set movement true if has a value in either direction
        if (IsMoving != movedLastFrame)
        { //Invoke the event when a change in state occurs
            SetIsMoving?.Invoke(IsMoving);
        }
    }

    //Setters
    public abstract void SetMoveDirection(Vector2 moveDirection);
    public void SetCanMove(bool _canMove) { CanMove = _canMove; }
    public void SetSpeedModifier(float _speedModifier) { SpeedModifier = _speedModifier; } //Don't want it to be negative
}
