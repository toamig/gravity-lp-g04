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

    // Player
    private Player _player;
    public Player player => _player;

    // Managers
    private InputManager _inputManager;
    public InputManager inputManager => _inputManager;

    private LevelManager _levelManager;
    public LevelManager levelManager => _levelManager;

    private BlackHoleManager _blackHoleManager;
    public BlackHoleManager blackHoleManager => _blackHoleManager;

    private int _currentLevel;

    public GameObject UI;

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

        _currentLevel = SceneManager.GetActiveScene().buildIndex;
        GameEvents.instance.OnPlayerLaunched += () => _levelStarted = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _levelStarted = false;
        Time.timeScale = 1;

        if (scene.buildIndex != 0)
        {
            Instantiate(UI);
            InitializeManagers();
        }

        if (scene.buildIndex != _currentLevel)
        {
            NextLevel();
        }
        else
        {
            RestartLevel();
        }

        _currentLevel = scene.buildIndex;
    }

    void InitializeManagers()
    {
        _player = GameObject.FindObjectOfType<Player>();
        _inputManager = GetComponent<InputManager>();
        _blackHoleManager = GetComponent<BlackHoleManager>();
        _levelManager = GameObject.FindObjectOfType<LevelManager>();
    }

    void NextLevel()
    {
        _inputManager.UpdateReferences();
        _blackHoleManager.ResetBlackHoles();
    }

    void RestartLevel()
    {
        _blackHoleManager.UpdateBlackHoles();
        _inputManager.UpdateReferences();
    }
}