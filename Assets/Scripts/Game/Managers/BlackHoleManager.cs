using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackHoleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _blackHolePrefab;
    public GameObject blackHolePrefab => _blackHolePrefab;

    private List<BlackHole> _blackHoleList;
    public List<BlackHole> blackHoleList
    {
        get => _blackHoleList;
        set => _blackHoleList = value;
    }

    private void Awake()
    {
        _blackHoleList = new List<BlackHole>();
        GameEvents.instance.OnSceneChanged += ResetBlackHoles;
        GameEvents.instance.OnSceneRealoaded += UpdateBlackHoles;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetBlackHoles()
    {
        foreach (BlackHole bh in _blackHoleList)
        {
            Destroy(bh.gameObject);
        }
        _blackHoleList = new List<BlackHole>();
    }

    private void UpdateBlackHoles()
    {
        foreach (BlackHole bh in _blackHoleList)
        {
            bh.UpdatePlayer();
        }
    }
}
