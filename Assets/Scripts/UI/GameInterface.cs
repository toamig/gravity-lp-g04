using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameInterface : MonoBehaviour
{
    public Text level;

    public Text blackHoles;

    public Button retry;

    private void Awake()
    {
        GameEvents.instance.OnBlackHolePlaced += UpdateBlackHoleCounter;
        GameEvents.instance.OnBlackHoleRemoved += UpdateBlackHoleCounter;
        retry.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single));
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeUI()
    {
        level.text = SceneManager.GetActiveScene().buildIndex.ToString();
        blackHoles.text = GameManager.instance.blackHoleManager.blackHoleList.Count + "/" + GameManager.instance.levelManager.blackHoleNumber;
    }

    void UpdateBlackHoleCounter(int num)
    {
        this.blackHoles.text = num + "/" + GameManager.instance.levelManager.blackHoleNumber;
    }

    private void OnDestroy()
    {
        GameEvents.instance.OnBlackHolePlaced -= UpdateBlackHoleCounter;
        GameEvents.instance.OnBlackHoleRemoved -= UpdateBlackHoleCounter;
        //Debug.Log("destruiu");
    }
}
