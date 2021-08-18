using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        if(Camera.main.TryGetComponent(out CameraFollow cameraFollow))
        {
            cameraFollow.SetTarget(transform);
        }
    }

    public void EnterRoom(Transform loadPosition)
    {
        transform.position = loadPosition.position;
    }
}
