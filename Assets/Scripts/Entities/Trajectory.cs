using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    private GameObject _trajectoryObject;

    [SerializeField]
    private int _numDots;
    public int numDots => _numDots;

    [SerializeField]
    private float _dotSpacing;
    public float dotSpacing => _dotSpacing;

    [SerializeField]
    private GameObject _dotObject;
    public GameObject dotObject => _dotObject;

    [SerializeField] [Range(0.01f, 0.3f)]
    private float _dotMinScale;
    public float dotMinScale => _dotMinScale;

    [SerializeField] [Range(0.3f, 1f)]
    private float _dotMaxScale;
    public float dotMaxScale => _dotMaxScale;

    private void Awake()
    {
        GameEvents.instance.OnPlayerLaunched += Hide;
    }

    // Start is called before the first frame update
    void Start()
    {
        PrepareDots();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.levelStarted)
        {
            UpdateDots();
        }
    }

    void PrepareDots()
    {
        _trajectoryObject = new GameObject("Trajectory");

        dotObject.transform.localScale = Vector3.one * _dotMaxScale;

        float scale = _dotMaxScale;
        float scaleFactor = scale / _numDots;

        for (int i = 0; i < _numDots; i++)
        {
            GameObject dot = Instantiate(_dotObject, _trajectoryObject.transform, true);

            dot.transform.localScale = Vector3.one * scale;
            if(scale > _dotMinScale)
            {
                scale -= scaleFactor;
            }
        }
    }

    void UpdateDots()
    {
        float timeStamp = _dotSpacing;
        Vector2 pos = new Vector2();

        Vector2 playerPos = transform.position;

        for (int i = 0; i < _numDots; i++)
        {
            pos.x = (playerPos.x + GameManager.instance.levelManager.startVelocity.vector.x * timeStamp);
            pos.y = (playerPos.y + GameManager.instance.levelManager.startVelocity.vector.y * timeStamp);

            _trajectoryObject.transform.GetChild(i).position = pos;
            timeStamp += dotSpacing;
        }
    }

    private void OnDestroy()
    {
        GameEvents.instance.OnPlayerLaunched -= Hide;
    }

    void Show()
    {
        _trajectoryObject.SetActive(true);
    }

    void Hide()
    {
        _trajectoryObject.SetActive(false);
    }
}
