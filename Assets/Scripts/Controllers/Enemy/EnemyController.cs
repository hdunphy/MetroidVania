using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform CheckWallTransform;
    public PlayerController Player { get; private set; }

    /// <summary>
    /// What happens when the Enemy Entity has been killed
    /// </summary>
    public void OnDied()
    {
        Destroy(gameObject);
    }

    public void SetPlayer(PlayerController _player)
    {
        Player = _player;
    }

    public void Attack()
    {
        Debug.Log("Attack!");
    }
}
