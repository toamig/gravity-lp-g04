using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int _currentLevel = 1;
    public int currentLevel => _currentLevel;

    [SerializeField]
    private int _blackHoles = 5;
    public int blackHoles => _blackHoles;

    // Game manager singleton initialization
    private static GameManager _instance;
    public static GameManager instance => _instance;

    private InputManager _inputManager;
    public InputManager inputManager => _inputManager;

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

        if (scene.name == "Menu")
        {

        }

        if (scene.name == "MainScene")
        {
            InitializeManagers();
        }
    }

    void InitializeManagers()
    {
        _inputManager = GameObject.FindObjectOfType<InputManager>();
    }
}