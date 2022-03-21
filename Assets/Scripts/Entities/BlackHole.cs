using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField]
    private float _gravConst = 0.5f; 
    public float gravConst
    {
        get => _gravConst;
        set => _gravConst = value;
    }

    private float _mass = 20;
    public float mass
    {
        get => _mass;
        set => _mass = value;
    }

    private static float _minScale = 0.5f;
    public static float minScale => _minScale;

    private static float _maxScale = 3;
    public static float maxScale => _maxScale;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
