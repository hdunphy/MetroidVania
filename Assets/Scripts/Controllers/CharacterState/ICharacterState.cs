using System.Collections.Generic;

public enum CharacterState { Gounded, Airborn, WallSliding }

public interface ICharacterState
{
    void EnterState();
    void Update();
}
