using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class CollectablePickup : MonoBehaviour
{
    [SerializeField] private float ShakeDuration; //Duration of camera shake upon pickup
    [SerializeField] private UnityEvent OnPickupEvent; //Triggers on a successful pickup


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController controller))
        { //Check that the object has "Player" tag and has a CharacterController2d
            OnPickup(controller);
            if (Camera.main.TryGetComponent(out CameraFollow follow))
            {
                follow.ShakeCamera(ShakeDuration);
            }
            OnPickupEvent?.Invoke();
        }
    }

    public abstract void OnPickup(PlayerController controller);
}
