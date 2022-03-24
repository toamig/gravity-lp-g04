using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLevel => SceneManager.GetActiveScene().buildIndex;

    // Game manager singleton initialization
    private static GameManager _instance;
    public static GameManager instance => _instance;

    private InputManager _inputManager;
    public InputManager inputManager => _inputManager;

    private LevelManager _levelManager;
    public LevelManager levelManager => _levelManager;

    private bool _levelStarted;
    public bool levelStarted => _levelStarted;


    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        GameEvents.instance.OnPlayerLaunched += () => _levelStarted = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _levelStarted = false;

        if (scene.buildIndex != 0)
        {
            _levelManager = GameObject.FindObjectOfType<LevelManager>();
        }
        else
        {

        }
    }

    void InitializeManagers()
    {
        _inputManager = GameObject.FindObjectOfType<InputManager>();
    }
}