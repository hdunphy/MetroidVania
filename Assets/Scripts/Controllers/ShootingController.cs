using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingController : MonoBehaviour
{
    [SerializeField, Tooltip("Transform for where the bullets come from")] private Transform shotInitialPosition;
    [SerializeField, Tooltip("Triggers when the entity is shooting")] private UnityEvent OnShootEvent;

    private Vector2 direction; //will need this if we can shoot up
    public Transform ShotInitialPosition { get => shotInitialPosition; } //Getter for the Transform

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
        direction = Quaternion.AngleAxis(ShotInitialPosition.eulerAngles.z, Vector3.forward) * Vector3.right;
        //need the local scale for when the player flips. Player flips by changing the sign of localScale.x
        return new Vector2(transform.localScale.x * direction.x, direction.y);
    }

    /// <summary>
    /// Event used to let other components know the entity is shooting
    ///     Used for animation, sound, particles, etc
    /// </summary>
    public void TriggerOnShootEvent()
    {
        OnShootEvent?.Invoke();
    }
}
