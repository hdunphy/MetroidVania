using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private string InitialSceneToLoad; //Name of scene to load on start
    [SerializeField] private PlayerController Player; //Player prefab to instantiate on start
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
        StartCoroutine(LoadInitialScene(InitialSceneToLoad));
    }

    /// <summary>
    /// Which scene to load initially and all essential objects
    /// </summary>
    /// <param name="initialSceneName">Name of the scene to load</param>
    /// <returns>IEnumerator so it can be run as a coroutine</returns>
    private IEnumerator LoadInitialScene(string initialSceneName)
    {
        yield return SceneManager.LoadSceneAsync(initialSceneName, LoadSceneMode.Additive);

        Instantiate(Player, StartPosition, Quaternion.identity);
    }
}
