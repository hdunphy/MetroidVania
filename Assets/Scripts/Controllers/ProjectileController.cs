using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileController : DamageOnHit
{
    [SerializeField] private Rigidbody2D m_RigidBody2D;
    [SerializeField, Tooltip("Call Event when projectile collides with a valid object")] private UnityEvent OnObjectCollision;

    /// <summary>
    /// Set the velocity in the desired direction
    /// </summary>
    /// <param name="projectileSpeed">max scalar velocity of the projectile</param>
    /// <param name="direction">the direction the projectile should go in</param>
    public void SetVelocityandDirection(float projectileSpeed, Vector2 direction)
    {
        Vector2 velocity = direction.normalized * projectileSpeed;
        m_RigidBody2D.velocity = velocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (OnHit(collision.gameObject))
        {
            OnObjectCollision?.Invoke(); //Trigger the UnityEvent
            Destroy(gameObject); //Destroy self
        }
    }
}
