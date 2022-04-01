using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartInterface : MonoBehaviour
{

    public Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(() => SceneManager.LoadScene("Level1", LoadSceneMode.Single));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
