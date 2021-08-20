using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// What happens when the Enemy Entity has been killed
    /// </summary>
    public void OnDied()
    {
        Destroy(gameObject);
    }
}
