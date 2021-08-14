using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private float FollowSpeed = 2f;
    [SerializeField] private float ZValue;

    [Header("Camera Shake")]
    [SerializeField] private float ShakeAmount = 0.1f;
    [SerializeField] private float DecreaseFactor = 1.0f;

    private float shakeDuration;
    private Vector3 originalPosition;

    private void Awake()
    {
        if(Target == null)
        {
            Target = transform;
        }
    }

    private void OnEnable()
    {
        originalPosition = transform.localPosition;
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = Target.position;
        newPosition.z = ZValue;
        transform.position = Vector3.Slerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);

        if(shakeDuration > 0)
        {
            transform.localPosition = originalPosition + Random.insideUnitSphere * ShakeAmount;

            shakeDuration -= Time.deltaTime * DecreaseFactor;
        }
    }

    public void ShakeCamera(float _shakeDuration)
    {
        originalPosition = transform.localPosition;
        shakeDuration = _shakeDuration;
    }
}
