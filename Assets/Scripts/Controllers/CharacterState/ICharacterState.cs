using System.Collections.Generic;

public enum CharacterState { Gounded, Airborn, WallSliding }

public interface ICharacterState
{
    /// <summary>
    /// Triggers when this state has been entered
    /// </summary>
    void EnterState();

    /// <summary>
    /// Triggers every Update() on parent monobehavior
    /// </summary>
    void Update();
}
