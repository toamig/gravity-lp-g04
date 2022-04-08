using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private static float _minScale = 0.5f;
    public static float minScale => _minScale;

    private static float _maxScale = 3;
    public static float maxScale => _maxScale;

    [SerializeField]
    private float _gravConst = 1; 
    public float gravConst
    {
        get => _gravConst;
        set => _gravConst = value;
    }

    private bool _isPlayerInside;

    private Player _player;

    private void Awake()
    {
        _isPlayerInside = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _player = GameManager.instance.player;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.levelStarted && _isPlayerInside)
        {
            Attract(_player.gameObject);
        }

        // Manual collision

        //if ((GameManager.instance.player.transform.position - transform.position).magnitude <= (GetComponentsInChildren<CircleCollider2D>()[0].bounds.extents.x + GameManager.instance.player.GetComponent<CircleCollider2D>().bounds.extents.x))
        //{
        //    Debug.Log("COLLISION");
        //}
    }

    void Attract(GameObject attractedObj)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Rigidbody2D rbAttracted = attractedObj.GetComponent<Rigidbody2D>();

        Vector2 direction = (transform.position - attractedObj.transform.position);
        float distance = direction.magnitude;
        float actualdistance = distance;
        if (distance <= 1) distance = 1;
        float forceMagnitude = _gravConst * (rb.mass * rbAttracted.mass) / (Mathf.Pow(distance,2) * actualdistance);
        Vector2 force = direction * forceMagnitude;

        rbAttracted.AddForce(force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _isPlayerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _isPlayerInside = false;
        }
    }

    public void UpdateBlackHole()
    {
        _isPlayerInside = false;
        _player = GameManager.instance.player;
    }
}
