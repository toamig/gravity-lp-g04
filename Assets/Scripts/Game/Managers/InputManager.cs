using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private List<BlackHole> _blackHoleList;

    private BlackHole _lastBlackHole;


    private void Awake()
    {
        
    }

    private void Start()
    {
        _blackHoleList = GameManager.instance.blackHoleManager.blackHoleList;
    } 

    void Update()
    {
        // Player launch

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameEvents.instance.PlayerLaunched();
        }

        // Prevent inputs when over UI

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        // Black hole placement

        if (!GameManager.instance.levelStarted)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                if (_blackHoleList.Count < GameManager.instance.levelManager.blackHoleNumber)
                {
                    _lastBlackHole = PlaceBlackHole(mousePosition);
                    _blackHoleList.Add(_lastBlackHole);
                    GameEvents.instance.BlackHolePlaced(_blackHoleList.Count);
                }
                else
                {
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
                    _lastBlackHole.transform.localScale = Vector3.one * scale;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _lastBlackHole = null;
            }

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.name == "Center" && hit.collider.tag == "BlackHole")
                {
                    Debug.Log("entrou");
                    Transform bhTransform = GetComponentInParent<Transform>();
                    bhTransform.position = mousePosition;
                }
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    if (_blackHoleList.Count > 0)
                    {
                        Destroy(_blackHoleList[_blackHoleList.Count - 1].gameObject);
                        _blackHoleList.RemoveAt(_blackHoleList.Count - 1);
                        GameEvents.instance.BlackHoleRemoved(_blackHoleList.Count);
                    }
                }
            }
        }

        // Camera control

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 2;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5, 10);

    }


    BlackHole PlaceBlackHole(Vector2 position)
    {
        GameObject blackHolePrefab = GameManager.instance.blackHoleManager.blackHolePrefab;

        GameObject bh = Instantiate(blackHolePrefab, position, Quaternion.identity);
        bh.transform.localScale = Vector3.one * BlackHole.minScale;
        DontDestroyOnLoad(bh);
        return bh.GetComponent<BlackHole>();
    }
}
