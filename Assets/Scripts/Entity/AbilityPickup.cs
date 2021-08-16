using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AbilityPickup : MonoBehaviour
{
    /*Component attached to an ingame ability pickup*/

    [SerializeField] private Ability Ability; //Ability given to player upon pickup
    [SerializeField] private float ShakeDuration; //Duration of camera shake upon pickup

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.TryGetComponent(out CharacterController2D controller))
        { //Check that the object has "Player" tag and has a CharacterController2d
            controller.AddAbility(Ability);
            if(Camera.main.TryGetComponent(out CameraFollow follow))
            {
                follow.ShakeCamera(ShakeDuration);
            }
            //After adding the ability, destroy the object from the scene
            Destroy(gameObject);
        }
    }
}
