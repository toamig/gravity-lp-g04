using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _playerLaunched;

    private void Awake()
    {
        GameEvents.instance.OnPlayerLaunched += LaunchPlayer;
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

    private void OnDestroy()
    {
        GameEvents.instance.OnPlayerLaunched -= LaunchPlayer;
    }

    void OnDrawGizmos()
    {
        Vector3 vel = GetComponent<Rigidbody2D>().velocity;
        Gizmos.DrawLine(transform.position, transform.position + vel);
    }
}
