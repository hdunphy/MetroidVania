using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform CheckWallTransform;
    /// <summary>
    /// What happens when the Enemy Entity has been killed
    /// </summary>
    public void OnDied()
    {
        Destroy(gameObject);
    }
}
