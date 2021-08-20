using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private List<EnemyBehaviorEnum> EnemyBehaviors;
    [SerializeField] private EnemyBehaviorEnum CurrentState;
    [SerializeField] private IEnemyBehaviour test;

    private Dictionary<EnemyBehaviorEnum, IEnemyBehaviour> BehaviorStateMachine;

    private void Start()
    {
        BehaviorStateMachine = new Dictionary<EnemyBehaviorEnum, IEnemyBehaviour>();

        foreach(EnemyBehaviorEnum behaviorEnum in EnemyBehaviors)
        {
            BehaviorStateMachine.Add(behaviorEnum, EnemyBehaviourFactory.AddEnemyBehaviour(behaviorEnum, gameObject));
        }
    }

    private bool init = false;

    private void Update()
    {
        if (!init)
        {
            BehaviorStateMachine[CurrentState].EnterState();
            init = true;
        }
    }
}
