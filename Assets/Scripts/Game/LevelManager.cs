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
}
