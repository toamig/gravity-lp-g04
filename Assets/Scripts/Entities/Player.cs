using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _velocity;
    public Vector3 velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    Rigidbody2D bhrb2d;

    public float gravConst = 9.8f;
    private float pi = Mathf.PI;
    private float radius;

    private List<GameObject> blackHoles = new List<GameObject>();

    private bool _playerLaunched;

    private void Awake()
    {
        GameEvents.instance.OnPlayerLaunched += LaunchPlayer;
        _playerLaunched = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        foreach (GameObject hole in blackHoles)
        {
            Transform bhTransform = GetComponentInParent<Transform>();
            radius = bhTransform.localScale.x/2;
            rb2d.AddForce(HoleGravityVector(hole.transform.position, transform.position, 25*radius*radius*pi), ForceMode2D.Impulse); ;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlackHole" && collision.gameObject.name == "Range")
        {
            blackHoles.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlackHole" && collision.gameObject.name == "Range")
        {
            blackHoles.Remove(collision.gameObject);
        }
    }

    void LaunchPlayer()
    {
        if (!_playerLaunched)
        {
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.AddForce(PlayerLaunchVector(0, 50), ForceMode2D.Impulse);
            _playerLaunched = true;
        }
    }

    Vector2 PlayerLaunchVector(float angle, float strength)
    {
        Vector2 launch;
        launch.x = Mathf.Cos(angle);
        launch.y = Mathf.Sin(angle);
        launch *= strength;
        return launch;
    }

    Vector2 HoleGravityVector(Vector2 holevector, Vector2 objvector, float holemass)
    {
        Vector2 obj2hole;

        obj2hole.x = holevector.x - objvector.x;
        obj2hole.y = holevector.y - objvector.y;
        float distance = obj2hole.magnitude;
        obj2hole.Normalize();
        float force = gravConst * holemass / (distance * distance);
        obj2hole *= force;

        return obj2hole;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + _velocity);
    }
}
