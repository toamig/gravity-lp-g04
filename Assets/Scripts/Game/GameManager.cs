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

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    void Update()
    {

    }

    void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Menu")
        {

        }

        if (arg0.name == "MainScene")
        {
            InitializeManagers();
        }
    }

    void InitializeManagers()
    {
        _inputManager = GameObject.FindObjectOfType<InputManager>();
    }

    public float GetNumBlackHoles()
    {
        return GameObject.FindObjectsOfType<BlackHole>().Length;
    }


}