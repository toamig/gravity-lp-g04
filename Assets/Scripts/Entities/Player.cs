using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool _playerLaunched;

    private void Awake()
    {
        GameEvents.instance.OnPlayerLaunched += LaunchPlayer;
        GameEvents.instance.OnPlayerDeath += KillPlayer;
        _playerLaunched = false;
    }

    private void Update()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        Vector3 vel = rb2d.velocity;
        float lookAngle = Vector3.SignedAngle(Vector3.right, vel, Vector3.forward);
        rb2d.SetRotation(lookAngle);
    }

    void LaunchPlayer()
    {
        if (!_playerLaunched)
        {
            Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
            rb2d.AddForce(GameManager.instance.levelManager.startVelocity.vector, ForceMode2D.Impulse);
            _playerLaunched = true;
        }
    }

    void KillPlayer()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = Vector2.zero;
        rb2d.AddTorque(100);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Center" && collision.gameObject.tag == "BlackHole")
        {
            GameEvents.instance.PlayerDeath();
        }
    }

    private void OnDestroy()
    {
        GameEvents.instance.OnPlayerLaunched -= LaunchPlayer;
        GameEvents.instance.OnPlayerDeath -= KillPlayer;
    }

    void OnDrawGizmos()
    {
        Vector3 vel = GetComponent<Rigidbody2D>().velocity;
        Gizmos.DrawLine(transform.position, transform.position + vel);
    }
}
