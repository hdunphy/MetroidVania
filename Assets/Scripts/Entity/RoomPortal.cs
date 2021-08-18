using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RoomIdentifier { A, B, C, D, E }

public class RoomPortal : MonoBehaviour
{
    [SerializeField] private string SceneToLoad;
    [SerializeField] private Transform LoadPosition;
    [SerializeField] private RoomIdentifier RoomIdentifier;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerController playerController))
        {
            StartCoroutine(SwitchScene(SceneToLoad, playerController));
        }
    }

    private IEnumerator SwitchScene(string sceneName, PlayerController playerController)
    {
        Debug.Log($"Loading Scene: {sceneName}, From Scene: {gameObject.scene.buildIndex}");

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        RoomPortal connectingPortal = FindObjectsOfType<RoomPortal>().First(x => x != this && x.RoomIdentifier == RoomIdentifier);
        playerController.EnterRoom(LoadPosition);

        yield return SceneManager.UnloadSceneAsync(gameObject.scene.name);
    }
}
