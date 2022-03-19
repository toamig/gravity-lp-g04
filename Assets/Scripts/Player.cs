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

    private Vector3 _oldVelocity;

    private bool _inBlackHole = false;

    GameObject blackHole;
    BlackHole bh;

    public float gravConst = 9.8f;

    


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.AddForce(PlayerLaunchVector(0, 50), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (_inBlackHole)
        {
            rb2d.AddForce(HoleGravityVector(blackHole.transform.position, transform.position, bh.mass), ForceMode2D.Impulse);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlackHole")
        {
            Debug.Log("ENTROU");

            blackHole = collision.gameObject;
            bh = blackHole.GetComponentInParent<BlackHole>();
            _inBlackHole = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlackHole")
        {
            _inBlackHole = false;
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
