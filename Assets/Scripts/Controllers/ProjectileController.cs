using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : DamageOnHit
{
    [SerializeField] private Rigidbody2D m_RigidBody2D;
    [SerializeField] private UnityEvent OnObjectCollision;

    public void SetVelocityandDirection(float projectileSpeed, Vector2 direction)
    {
        Vector2 velocity = direction.normalized * projectileSpeed;
        m_RigidBody2D.velocity = velocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnHit(collision.gameObject))
        {
            OnObjectCollision?.Invoke();
            Destroy(gameObject);
        }
    }
}
