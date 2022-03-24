using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject blackHolePrefab;

    private List<GameObject> _blackHoleList;

    private GameObject _lastBlackHole;

    private void Awake()
    {
        _blackHoleList = new List<GameObject>();
    }

    void Update()
    {
        // Black Hole Placement

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (_blackHoleList.Count < GameManager.instance.levelManager.blackHoleNumber)
            {
                _lastBlackHole = PlaceBlackHole(mousePosition);
                _blackHoleList.Add(_lastBlackHole);
                GameEvents.instance.BlackHolePlaced(_blackHoleList.Count);
            }
            else {
                _lastBlackHole = null;
                Debug.Log("You can't place more black holes!");
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (_lastBlackHole != null)
            {
                Vector3 mousePos3 = mousePosition;
                float mouseDiff = (_lastBlackHole.transform.position - mousePos3).magnitude;
                float scale = Mathf.Min(mouseDiff, BlackHole.maxScale);
                scale = Mathf.Max(scale, BlackHole.minScale);
                _lastBlackHole.transform.localScale = new Vector3(scale, scale, scale);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _lastBlackHole = null;
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (_blackHoleList.Count > 0)
                {
                    Destroy(_blackHoleList[_blackHoleList.Count - 1]);
                    _blackHoleList.RemoveAt(_blackHoleList.Count - 1);
                    GameEvents.instance.BlackHoleRemoved(_blackHoleList.Count);
                }
            }
        }

        // Player Launch

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEvents.instance.PlayerLaunched();
        }

        // Camera Control

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 2;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5, 10);

    }

    GameObject PlaceBlackHole(Vector2 position)
    {
        GameObject bh = Instantiate(blackHolePrefab, position, Quaternion.identity);
        bh.transform.localScale = new Vector3(BlackHole.minScale, BlackHole.minScale, BlackHole.minScale);
        return bh;
    }
}
