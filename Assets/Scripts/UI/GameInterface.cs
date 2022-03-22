using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    public Text level;

    public Text blackHoles;

    public Button retry;

    private void Awake()
    {
        GameEvents.instance.OnBlackHolePlaced += UpdateBlackHoleCounter;
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
        level.text = "1";
        blackHoles.text = "0/" + GameManager.instance.blackHoles;
    }

    void UpdateBlackHoleCounter()
    {
        blackHoles.text = GameManager.instance.GetNumBlackHoles() + "/" + GameManager.instance.blackHoles;
    }
}
