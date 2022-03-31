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
