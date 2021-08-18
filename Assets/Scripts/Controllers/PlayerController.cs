using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        //Connect main camera to player
        if(Camera.main.TryGetComponent(out CameraFollow cameraFollow))
        {
            cameraFollow.SetTarget(transform);
        }
    }

    /// <summary>
    /// Move player to new room
    /// </summary>
    /// <param name="loadPosition">The transform for where the player should move to in new room</param>
    public void EnterRoom(Transform loadPosition)
    {
        transform.position = loadPosition.position;
    }
}
