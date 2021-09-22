using UnityEngine;

public interface IEntityMovement
{
    void SetMoveDirection(Vector2 moveDirection);
    void SetSpeedModifier(float speedModifier);
    void SetCanMove(bool canMove);
}
