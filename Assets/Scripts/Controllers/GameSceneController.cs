using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameSceneController : MonoBehaviour
{
    [SerializeField] private string InitialSceneToLoad; //Name of scene to load on start
    [SerializeField] private CameraFollow CameraPrefab; //Camera Prefab to load
    [SerializeField] private PlayerController PlayerPrefab; //Player prefab to instantiate on start
    [SerializeField] private Vector3 StartPosition; //Start position of the player
    [SerializeField] private Transform EssentialObjectTransform; //Object to parent instantiated objects
    [SerializeField] private bool IsTestingMode = true; //True if starting from this screen and want to start game on load

    public static GameSceneController Singleton;
    public Vector3 _startPosition { get => StartPosition; }
    public GameState CurrentGameState { get; private set; }
    public static string MainMenuScene { get => "MainMenu"; }

    public enum GameState { InGame, Paused, Menu }

    private void Awake()
    {
        //Singleton pattern On Awake set the singleton to this.
        //There should only be one GameLayer that can be accessed statically
        if (Singleton == null)
        {
            Singleton = this;
            CurrentGameState = GameState.Menu; //initialize to Menu since first loads when at main menu
        }
        else
        { //if Gamelayer already exists then destory this. We don't want duplicates
            Destroy(this);
        }
    }

    private void Start()
    {
        if (IsTestingMode)
            StartGame();
    }

    /// <summary>
    /// Start the game
    /// Can also be used to reload the game upon death
    /// </summary>
    public void StartGame()
    {
        if(CurrentGameState == GameState.Menu)
        {
            string path = Application.persistentDataPath + "/saves/" + SaveData.current.SaveName + ".save";
            SaveData.current = (SaveData)SerializationManager.Load(path);

            Debug.Log("Loaded");
        }

        CurrentGameState = GameState.InGame;

        InitialSceneToLoad = string.IsNullOrEmpty(SaveData.current.PlayerSceneName) ?
            InitialSceneToLoad : SaveData.current.PlayerSceneName;

        if (SaveData.current.PlayerPosition != Vector3.zero)
            StartPosition = SaveData.current.PlayerPosition;

        StartCoroutine(LoadInitialScene(InitialSceneToLoad));
    }

    /// <summary>
    /// Load/Unload the pause menu
    /// </summary>
    /// <param name="isPaused"></param>
    public void SetPaused(bool isPaused)
    {
        CurrentGameState = isPaused ? GameState.Paused : GameState.InGame;

        if (CurrentGameState == GameState.Paused)
        {
            Time.timeScale = 0;
            FindObjectOfType<PlayerInputController>().EnableInput(false);

            StartCoroutine(LoadSceneAndThen(MainMenuScene, LoadSceneMode.Additive,
                () => FindObjectOfType<MenuCameraController>().SetCameraPosition(FindObjectOfType<CameraFollow>().transform.position)));
        }
        else if (CurrentGameState == GameState.InGame)
        {
            SceneManager.UnloadSceneAsync(MainMenuScene);
            Time.timeScale = 1;
            FindObjectOfType<PlayerInputController>().EnableInput(true);
        }
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1;//game was coming from pause menu
        UnloadAllScenesExcept(gameObject.scene.name);
        CurrentGameState = GameState.Menu;

        SceneManager.LoadScene(MainMenuScene);
    }

    /// <summary>
    /// Which scene to load initially and all essential objects
    /// </summary>
    /// <param name="initialSceneName">Name of the scene to load</param>
    /// <returns>IEnumerator so it can be run as a coroutine</returns>
    private IEnumerator LoadInitialScene(string initialSceneName)
    {
        var cam = EssentialObjectTransform.GetComponentInChildren<CameraFollow>();
        if (cam == null)
        { //Make sure the camera is in the scene
            cam = Instantiate(CameraPrefab, EssentialObjectTransform);
            cam.transform.position = StartPosition + (Vector3.back * 10);
        }

        var player = EssentialObjectTransform.GetComponentInChildren<PlayerController>();
        if (player == null)
        { //Make sure the player is in the scene
            player = Instantiate(PlayerPrefab, EssentialObjectTransform);
        }

        if (SceneManager.GetActiveScene().name != initialSceneName)
        { //Make sure the scene isn't already loaded
            yield return SceneManager.LoadSceneAsync(initialSceneName, LoadSceneMode.Additive);
        }

        player.OnLoad(StartPosition);

        foreach (var sceneObject in FindObjectsOfType<SceneObjectState>())
        {
            sceneObject.OnLoad();
        }
    }

    public void LoadLastSave(PlayerController player)
    {
        UnloadAllScenesExcept(gameObject.scene.name);
        StartCoroutine(LoadLastSaveCoroutine(player));
    }

    private IEnumerator LoadLastSaveCoroutine(PlayerController player)
    {
        //remove connection main camera to player
        if (Camera.main.TryGetComponent(out CameraFollow cameraFollow))
        {
            cameraFollow.SetTarget(cameraFollow.transform);
        }
        Destroy(player.gameObject);

        yield return new WaitForSeconds(0.5f);

        StartGame();
    }

    private void UnloadAllScenesExcept(string sceneName)
    {
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (scene.name != sceneName)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    private IEnumerator LoadSceneAndThen(string sceneName, LoadSceneMode mode, Action action)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        action.Invoke();
    }
}
