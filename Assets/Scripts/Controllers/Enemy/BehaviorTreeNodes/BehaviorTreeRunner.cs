using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeRunner : MonoBehaviour
{
    public BehaviorTree tree;

    private void Start()
    {
        var enemyController = GetComponent<EnemyController>();

        tree = tree.Clone();
        tree.Bind(enemyController);
        tree.blackboard.AddParameter("EntityMovement", GetComponent<EntityMovement>());
        tree.blackboard.AddParameter("MovementDirection", 1f);
        tree.blackboard.AddParameter("CheckTransform", enemyController.CheckTransform);
        tree.blackboard.AddParameter("GroundLayer", GameLayers.Singleton.GroundLayer);
    }

    private void Update()
    {
        tree.Update();
    }
}
