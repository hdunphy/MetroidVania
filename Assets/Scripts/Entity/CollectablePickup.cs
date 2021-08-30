using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class CollectablePickup : MonoBehaviour
{
    [SerializeField, Tooltip("GUID used by save system")] private string GUID;
    [SerializeField] private float ShakeDuration; //Duration of camera shake upon pickup

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController controller))
        { //Check that the object has "Player" tag and has a CharacterController2d
            OnPickup(controller);
            if (Camera.main.TryGetComponent(out CameraFollow follow))
            {
                follow.ShakeCamera(ShakeDuration);
            }
            //After adding the ability, destroy the object from the scene
            Destroy(gameObject);
        }
    }

    public abstract void OnPickup(PlayerController controller);
}
