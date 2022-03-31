using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
