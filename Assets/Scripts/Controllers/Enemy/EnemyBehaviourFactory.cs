using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourFactory
{
    public static IEnemyBehaviour AddEnemyBehaviour(EnemyBehaviorEnum behaviourType, GameObject gameObject)
    {
        IEnemyBehaviour behaviour;

        switch (behaviourType)
        {
            case EnemyBehaviorEnum.Patrol:
                behaviour = gameObject.AddComponent<EnemyPatrol>();
                break;
            default:
                behaviour = gameObject.AddComponent<EnemyPatrol>();
                break;
        }

        return behaviour;
    }
}
