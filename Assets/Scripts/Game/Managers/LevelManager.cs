using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int _blackHoleNumber;
    public int blackHoleNumber => _blackHoleNumber;

    [SerializeField]
    private Vector2 _levelBorders;
    public Vector2 levelBorders => _levelBorders;


    [Header("Player")]
    [SerializeField]
    private Velocity _startVelocity;
    public Velocity startVelocity => _startVelocity;

    [Serializable]
    public struct Velocity
    {
        [SerializeField]
        private Vector2 _direction;
        public Vector2 direction => _direction.normalized;

        [SerializeField]
        private float _magnitude;
        public float magnitude => _magnitude;

        public Vector2 vector => direction * _magnitude;
    }
}
