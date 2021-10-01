using UnityEngine;

public interface IPathFinding
{
    void UpdatePath(Vector2 target);
    Vector2 GetDirection();
    void Initialize();
    int StepsLeftInPath();
}

public enum PathFindingState { Hunting, Stuck, Idle };