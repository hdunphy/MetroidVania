using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Target; //Target for the camera to follow
    [SerializeField] private float FollowSpeed = 2f; //Max Speed the camera will follow the target at
    [SerializeField] private float ZValue; //The position.z value the camera will stay at

    [Header("Camera Shake")]
    [SerializeField] private float ShakeAmount = 0.1f; //Maximum camera movement during camera shake
    [SerializeField] private float DecreaseFactor = 1.0f; //Decrease Factor for how quickly the camera stops shaking

    private float shakeDuration; //Private variable to keep track of current shake duration
    private Vector3 originalPosition; //store local position of the camera

    private void Awake()
    {
        if(Target == null)
        { //If no Target exists set the target to its own transform
            Target = transform;
        }
    }

    private void OnEnable()
    {
        //Store the original local position as soon as CameraFollow component is enabled
        originalPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        //Must use in Fixed Update otherwise there is jittering from the camera
        Vector3 newPosition = Target.position; //Get the targets location
        newPosition.z = ZValue; //Make sure to keep the z value the same otherwise camera will zoom towards the targets z value and will lose sight of the object
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime); //Interpolate from current pos to target pos

        if(shakeDuration > 0)
        { //Check if camera should be shaking

            //Get random value from unit sphere and add shake amount to it
            transform.localPosition = originalPosition + Random.insideUnitSphere * ShakeAmount;

            //Decrement the shake duration
            shakeDuration -= Time.deltaTime * DecreaseFactor;
        }
    }

    /// <summary>
    /// Used for other components to trigger the shake camera function
    /// </summary>
    /// <param name="_shakeDuration">Duration of how long the camera should shake for</param>
    public void ShakeCamera(float _shakeDuration)
    {
        originalPosition = transform.localPosition;
        shakeDuration = _shakeDuration;
    }
}
