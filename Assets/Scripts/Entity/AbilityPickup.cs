using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AbilityPickup : MonoBehaviour
{
    [SerializeField] private Ability Ability;
    [SerializeField] private float ShakeDuration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out CharacterController2D controller))
        {
            controller.AddAbility(Ability);
            if(Camera.main.TryGetComponent(out CameraFollow follow))
            {
                follow.ShakeCamera(ShakeDuration);
            }
            Destroy(gameObject);
        }
    }
}
