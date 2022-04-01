using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinCard : MonoBehaviour
{
    public Text LvlText;
    public Text Score;
    public Button NextButton;
    public Button RestartButton;

    void Awake()
    {
        GameEvents.instance.OnReachGoal += InitializeUI;
        NextButton.onClick.AddListener(() => SceneManager.LoadScene(GameManager.instance.currentLevel + 1, LoadSceneMode.Single));
        RestartButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single));
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int CalculateScore(int holeCount, int parNumber)
    {
        int diff = parNumber - holeCount;
        if (diff >= 0) return 3;
        else if (diff < -4) return 0;
        else return 3 + diff;
    }

    void InitializeUI()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        int holeCount = GameManager.instance.blackHoleManager.blackHoleList.Count;
        int parNumber = GameManager.instance.levelManager.parHoleNumber;
        int score = CalculateScore(holeCount, parNumber);
        Score.text = "Score:" + score;
    }

    void OnDestroy()
    {
        GameEvents.instance.OnReachGoal -= InitializeUI;
    }
}

