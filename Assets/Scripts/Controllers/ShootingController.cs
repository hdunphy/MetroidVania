using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingController : MonoBehaviour
{
    [SerializeField, Tooltip("Transform for where the bullets come from")] private Transform shotInitialPosition;
    [SerializeField, Tooltip("Triggers when the entity is preparing to shoot")] private UnityEvent OnShootInitEvent;
    [SerializeField, Tooltip("Triggers when the entity is shooting")] private UnityEvent OnShootEvent;

    private Vector2 direction; //will need this if we can shoot up
    private ProjectileController projectilePrefab;
    private float projectileSpeed;


    private void Start()
    {
        direction = Vector2.right; //Player initializes facing right so direction will be (1, 0)
    }

    /// <summary>
    /// Get the direction of the gun, to determine the path of the projectile
    /// </summary>
    /// <returns>Vector 2 of the direction the projectile should follow</returns>
    public Vector2 GetDirection()
    {
        direction = Quaternion.AngleAxis(shotInitialPosition.eulerAngles.z, Vector3.forward) * Vector3.right;
        //need the local scale for when the player flips. Player flips by changing the sign of localScale.x
        return new Vector2(transform.localScale.x * direction.x, direction.y);
    }

    /// <summary>
    /// Trigger animation and set the projectile & projectile speed
    /// </summary>
    /// <param name="_projectilePrefab">Sets the projectile prefab to shoot</param>
    /// <param name="_projectileSpeed">Sets the projectile speed when shooting</param>
    public void TriggerOnShootInit(ProjectileController _projectilePrefab, float _projectileSpeed)
    {
        projectilePrefab = _projectilePrefab;
        projectileSpeed = _projectileSpeed;
        OnShootInitEvent?.Invoke(); //Trigger animation
    }

    /// <summary>
    /// Instatiate Shooting projectile object and then
    /// trigger event used to let other components know the entity is shooting
    ///     Used for sound, particles, etc
    /// </summary>
    public void TriggerOnShootEvent()
    {
        ProjectileController projectile = Instantiate(projectilePrefab, shotInitialPosition.position, shotInitialPosition.rotation);
        projectile.SetVelocityandDirection(projectileSpeed, GetDirection());
        OnShootEvent?.Invoke();
    }
}
