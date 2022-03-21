using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _blackHoles = 5;
    public int blackHoles => _blackHoles;

    // Game manager singleton initialization
    private static GameManager _instance;
    public static GameManager instance => _instance;

    private InputManager _inputManager;
    public InputManager inputManager => _inputManager;


    public void Awake()
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

    private void Update()
    {

    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == "Menu")
        {

        }

        if (arg0.name == "MainScene")
        {
            InitializeManagers();
        }
    }

    public void InitializeManagers()
    {
        _inputManager = GameObject.FindObjectOfType<InputManager>();
    }


}