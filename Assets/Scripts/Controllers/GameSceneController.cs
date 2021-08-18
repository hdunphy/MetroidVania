using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private string InitialSceneToLoad;
    [SerializeField] private PlayerController Player;
    [SerializeField] private Vector3 StartPosition;

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

    private IEnumerator LoadInitialScene(string initialSceneName)
    {
        yield return SceneManager.LoadSceneAsync(initialSceneName, LoadSceneMode.Additive);

        Instantiate(Player, StartPosition, Quaternion.identity);
    }
}
