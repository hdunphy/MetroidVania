using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private EntityMovement EntityMovement;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;

    private void Start()
    {
        //Connect main camera to player
        if(Camera.main.TryGetComponent(out CameraFollow cameraFollow))
        {
            cameraFollow.SetTarget(transform);
        }

        EntityMovement = GetComponent<EntityMovement>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Leaving a room into another scene
    ///     Need to stop all movement
    ///     Block inputs so no abilities
    /// </summary>
    public void LeaveRoom()
    {
        m_Rigidbody2D.velocity = Vector2.zero;
        EntityMovement.enabled = false; //Disable movement so player cannot move until scene is done loading
    }

    /// <summary>
    /// Move player to new room
    /// </summary>
    /// <param name="loadPosition">The transform for where the player should move to in new room</param>
    public void EnterRoom(Transform loadPosition)
    {
        Debug.Log($"Pos: {loadPosition.position}");
        transform.position = loadPosition.position; //Move player to position
        Camera.main.transform.position = new Vector3(loadPosition.position.x, loadPosition.position.y, Camera.main.transform.position.z); //Move camera to position
        EntityMovement.enabled = true; //re-enable player movement
    }
}
