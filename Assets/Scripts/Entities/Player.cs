using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool _playerLaunched;
    private bool _playerDying;
    private float _deathTravel;
    private Vector3 _deathPosition;
    private Vector3 _killerHolePosition;
    private Color _currentColor;

    private void Awake()
    {
        GameEvents.instance.OnPlayerLaunched += LaunchPlayer;
        GameEvents.instance.OnPlayerDeath += KillPlayer;
        _playerLaunched = false;
        _playerLaunched = false;
    }

    private void Update()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        Vector3 vel = rb2d.velocity;
        Transform transform = GetComponent<Transform>();
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        if (_playerDying)
        {
            vel *= 0.0f;
            _deathTravel = Mathf.Clamp(_deathTravel, 0.0f, 0.99f);
            transform.Rotate(0, 0, 500 * Time.deltaTime * _deathTravel);
            transform.position = Vector3.Lerp(_deathPosition, _killerHolePosition, _deathTravel);
            _deathTravel += 0.6f * Time.deltaTime;
            _currentColor = spr.color;
            _currentColor.a = 1 - _deathTravel;
            spr.color = _currentColor;
        }
        else
        {
            float lookAngle = Vector3.SignedAngle(Vector3.right, vel, Vector3.forward);
            rb2d.SetRotation(lookAngle);
        }
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
        _playerDying = true;
        rb2d.isKinematic = true;
        _deathTravel = 0;
        _deathPosition = GetComponent<Transform>().position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Center" && collision.gameObject.tag == "BlackHole")
        {
            GameEvents.instance.PlayerDeath();
            _killerHolePosition = collision.gameObject.GetComponentInParent<Transform>().position;
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
