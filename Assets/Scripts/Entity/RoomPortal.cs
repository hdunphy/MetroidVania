using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

//Used to locate the connecting portal in the new scene
public enum RoomIdentifier { A, B, C, D, E }

public class RoomPortal : MonoBehaviour
{
    [SerializeField] private string SceneToLoad; //Name of the scene to load upon entering portal collider
    [SerializeField] private Transform LoadPosition; //Position for player to spawn at when coming out of this portal
    [SerializeField] private RoomIdentifier RoomIdentifier; //Enum to match up with portal in new scene

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController playerController))
        {
            StartCoroutine(SwitchScene(SceneToLoad, playerController));
        }
    }

    /// <summary>
    /// Coroutine to load the new scene and unload current scene
    /// </summary>
    /// <param name="sceneName">Name of scene to load. Scene must be a part of the build</param>
    /// <param name="playerController">The Player controller to move them into the new scene</param>
    /// <returns>IEnumerator for the coroutine</returns>
    private IEnumerator SwitchScene(string sceneName, PlayerController playerController)
    {
        Debug.Log($"Loading Scene: {sceneName}, From Scene: {gameObject.scene.buildIndex}");
        playerController.LeaveRoom();

        //Load new scene
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        //Once new scene is loaded, look for the connecting RoomPortal with the same identifier
        RoomPortal connectingPortal = FindObjectsOfType<RoomPortal>().First(x => x != this && x.RoomIdentifier == RoomIdentifier);
        playerController.EnterRoom(connectingPortal.LoadPosition.position); //Move the player to the load position of the new portal

        //Unload old scene
        yield return SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}
