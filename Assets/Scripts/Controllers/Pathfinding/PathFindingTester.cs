using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindingTester : MonoBehaviour
{
    public bool Test;
    public Transform Target;
    public Vector2 direction;

    private void OnValidate()
    {
#if (UNITY_EDITOR)
        if (Test)
        {
            Test = false;
            if(Target != null && TryGetComponent(out IPathFinding pathFinding))
            {
                pathFinding.Initialize();
                pathFinding.UpdatePath(Target.position);
                direction = pathFinding.GetDirection();
            }
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
