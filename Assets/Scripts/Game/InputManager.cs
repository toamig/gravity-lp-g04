using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject blackHolePrefab;

    private GameObject _blackHoleInstance;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            _blackHoleInstance = PlaceBlackHole(mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (_blackHoleInstance != null)
            {
                Vector3 mousePos3 = mousePosition;
                float scale = Mathf.Min((_blackHoleInstance.transform.position - mousePos3).magnitude, BlackHole.maxScale);
                scale = Mathf.Max(scale, BlackHole.minScale);
                _blackHoleInstance.transform.localScale = new Vector3(scale, scale, scale);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _blackHoleInstance = null;
            }
        }
        
    }

    GameObject PlaceBlackHole(Vector2 position)
    {
        GameObject bh = Instantiate(blackHolePrefab, position, Quaternion.identity);
        bh.transform.localScale = new Vector3(BlackHole.minScale, BlackHole.minScale, BlackHole.minScale);
        return bh;
    }

}
