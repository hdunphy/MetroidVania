using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private string InitialSceneToLoad; //Name of scene to load on start
    [SerializeField] private CameraFollow CameraPrefab; //Camera Prefab to load
    [SerializeField] private PlayerController PlayerPrefab; //Player prefab to instantiate on start
    [SerializeField] private Vector3 StartPosition; //Start position of the player

    public static GameSceneController Singleton;

    private void Awake()
    {
        //Singleton pattern On Awake set the singleton to this.
        //There should only be one GameLayer that can be accessed statically
        if (Singleton == null)
        {
            Singleton = this;
        }
        else
        { //if Gamelayer already exists then destory this. We don't want duplicates
            Destroy(this);
        }
    }

    private void Start()
    {
    }

    public void StartGame()
    {
        InitialSceneToLoad = string.IsNullOrEmpty(SaveData.current.PlayerSceneName) ?
            InitialSceneToLoad : SaveData.current.PlayerSceneName;
        StartCoroutine(LoadInitialScene(InitialSceneToLoad));
    }

    /// <summary>
    /// Which scene to load initially and all essential objects
    /// </summary>
    /// <param name="initialSceneName">Name of the scene to load</param>
    /// <returns>IEnumerator so it can be run as a coroutine</returns>
    private IEnumerator LoadInitialScene(string initialSceneName)
    {
        if (SceneManager.GetActiveScene().name != initialSceneName)
        { //Make sure the scene isn't already loaded
            yield return SceneManager.LoadSceneAsync(initialSceneName, LoadSceneMode.Additive);
        }

        if (Camera.main == null)
        { //Make sure the camera is in the scene
            Instantiate(CameraPrefab, StartPosition + (Vector3.back * 10), Quaternion.identity);
        }

        if (FindObjectOfType<PlayerController>() == null)
        { //Make sure the player is in the scene
            Instantiate(PlayerPrefab, StartPosition, Quaternion.identity);
        }
    }
}
