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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("BHs: " + _blackHoleList.Count);
        }
    }

    public void ResetBlackHoles()
    {
        foreach (BlackHole bh in _blackHoleList)
        {
            DestroyImmediate(bh.gameObject);
        }
        _blackHoleList = new List<BlackHole>();

        Debug.Log("OLA");
    }

    public void UpdateBlackHoles()
    {
        foreach (BlackHole bh in _blackHoleList)
        {
            bh.UpdateBlackHole();
        }
    }
}
