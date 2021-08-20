using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBehaviorEnum { Patrol }

public abstract class IEnemyBehaviour : MonoBehaviour
{
    public abstract void EnterState();
    public abstract void ExitState();
}
