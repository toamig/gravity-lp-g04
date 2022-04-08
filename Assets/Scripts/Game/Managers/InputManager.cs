using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class InputManager : MonoBehaviour
{
    private List<BlackHole> _blackHoleList;

    private bool _placedBlackHole;

    private Camera _mainCamera;

    private Vector3 dragOrigin;

    private void Awake()
    {
        _placedBlackHole = false;
        _mainCamera = Camera.main;
    }

    private void Start()
    {
        _blackHoleList = GameManager.instance.blackHoleManager.blackHoleList;
    } 

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosition3 = mousePosition;

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

            if (Input.GetMouseButtonDown(0))
            {
                if (_blackHoleList.Count < GameManager.instance.levelManager.blackHoleNumber)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                    if (hits.Length > 0)
                    {
                        if (hits.Count(i => i.collider.name != "Range") > 0)
                        {
                            Debug.Log("You can't place a black hole here!");
                        }
                        else
                        {
                            PlaceBlackHole(mousePosition);
                        }
                    }
                    else
                    {
                        PlaceBlackHole(mousePosition);
                    }
                }
                else
                {
                    Debug.Log("You can't place more black holes!");
                }
            }

            if (Input.GetMouseButton(0))
            {
                if (_placedBlackHole)
                {
                    BlackHole lastBlackHole = _blackHoleList[_blackHoleList.Count - 1];

                    float mouseDiff = (lastBlackHole.transform.position - mousePosition3).magnitude;
                    float scale = Mathf.Min(mouseDiff, BlackHole.maxScale);
                    scale = Mathf.Max(scale, BlackHole.minScale);
                    lastBlackHole.transform.localScale = Vector3.one * scale;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _placedBlackHole = false;
            }

            // Remove last black hole

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

            // Move black hole

            if (Input.GetMouseButton(1))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.tag == "BlackHole")
                    {
                        Rigidbody2D bhTransform = hit.collider.GetComponentInParent<Rigidbody2D>();
                        bhTransform.position = mousePosition;
                    }
                }
            }
        }

        // Camera control

        Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 2;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5, 10);

        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 diff = dragOrigin - Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 targetPos = Camera.main.transform.position + diff;

            float camX = Mathf.Clamp(targetPos.x, -GameManager.instance.levelManager.levelBorders.x / 2, GameManager.instance.levelManager.levelBorders.x / 2);
            float camY = Mathf.Clamp(targetPos.y, -GameManager.instance.levelManager.levelBorders.y / 2, GameManager.instance.levelManager.levelBorders.y / 2);

            Vector3 finalPos = new Vector3(camX, camY, targetPos.z);

            _mainCamera.transform.position = finalPos;
        }
    }


    void PlaceBlackHole(Vector2 position)
    {
        GameObject blackHolePrefab = GameManager.instance.blackHoleManager.blackHolePrefab;

        GameObject bh = Instantiate(blackHolePrefab, position, Quaternion.identity);
        bh.transform.localScale = Vector3.one * BlackHole.minScale;
        DontDestroyOnLoad(bh);
        _blackHoleList.Add(bh.GetComponent<BlackHole>());
        _placedBlackHole = true;
        GameEvents.instance.BlackHolePlaced(_blackHoleList.Count);
    }

    public void UpdateReferences()
    {
        _mainCamera = Camera.main;
        Debug.Log(_mainCamera);
        Debug.Log(Camera.main);
        _blackHoleList = GameManager.instance.blackHoleManager.blackHoleList;
    }
}
