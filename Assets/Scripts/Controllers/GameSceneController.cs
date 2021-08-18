using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private int InitialSceneToLoad;
    [SerializeField] private PlayerController Player;

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

    private IEnumerator LoadInitialScene(int initialSceneBuildIndex)
    {
        yield return SceneManager.LoadSceneAsync(initialSceneBuildIndex, LoadSceneMode.Additive);

        Instantiate(Player, Vector3.zero, Quaternion.identity);
    }
}
