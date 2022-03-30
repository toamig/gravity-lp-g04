using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    [SerializeField]
    private int _numDots;
    public int numDots => _numDots;

    [SerializeField]
    private float _dotSpacing;
    public float dotSpacing => _dotSpacing;

    [SerializeField]
    private GameObject _dotObject;
    public GameObject dotObject => _dotObject;

    // Start is called before the first frame update
    void Start()
    {
        PrepareDots();
        UpdateDots();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepareDots()
    {
        for (int i = 0; i < _numDots; i++)
        {
            Instantiate(_dotObject, gameObject.transform, true);
        }
    }

    void UpdateDots()
    {
        float timeStamp = _dotSpacing;
        Vector2 pos = new Vector2();

        Vector2 playerPos = GameManager.instance.player.gameObject.transform.localPosition;

        for (int i = 0; i < _numDots; i++)
        {
            pos.x = (playerPos.x + GameManager.instance.levelManager.startVelocity.vector.x * timeStamp);
            pos.x = (playerPos.y + GameManager.instance.levelManager.startVelocity.vector.y * timeStamp);

            
            transform.GetChild(i).position = pos;
            timeStamp += dotSpacing;
        }
    }
}
