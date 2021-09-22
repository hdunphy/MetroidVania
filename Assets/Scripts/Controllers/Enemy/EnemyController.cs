using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform CheckWallTransform;
    [SerializeField] private float PlayerFoundSpeedModifier = 1;
    private IEnemyAttack EnemyAttack;

    public PlayerController Player { get; private set; }
    public float SpeedModifier { get => PlayerFoundSpeedModifier; }

    private void Start()
    {
        EnemyAttack = GetComponent<IEnemyAttack>();
    }

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
        EnemyAttack.Attack();
    }
}
