using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private EntityMovement Movement;
    [SerializeField] private CharacterController2D CharacterController2D;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;

    private void Start()
    {
        //Connect main camera to player
        if(Camera.main.TryGetComponent(out CameraFollow cameraFollow))
        {
            cameraFollow.SetTarget(transform);
        }
    }

    /// <summary>
    /// Function called by UnityEvent to trigger this character's death
    ///     Will trigger any clean up, animations, and events that need to occur after this character dies
    /// </summary>
    public void OnDeath()
    {
        var damageable = GetComponent<Damageable>();
        if(damageable.currentHealth <= 0)
        {
            Debug.Log("Character is Dead");

            StartCoroutine(CharacterDeath());
        }
        else
        {
            CharacterController2D.Respawn();
        }
    }

    /// <summary>
    /// Coroutine for character death so some things are not instantanious like removing the object
    /// </summary>
    /// <returns></returns>
    IEnumerator CharacterDeath()
    {
        Movement.SetCanMove(false);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Leaving a room into another scene
    ///     Need to stop all movement
    ///     Block inputs so no abilities
    /// </summary>
    public void LeaveRoom()
    {
        m_Rigidbody2D.velocity = Vector2.zero;
        Movement.enabled = false; //Disable movement so player cannot move until scene is done loading
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
        Movement.enabled = true; //re-enable player movement
    }
}
