using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject BlackHole;
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseposition.z = 0;
            GameObject p = Instantiate(BlackHole, mouseposition, Quaternion.identity);

            Debug.Log(mouseposition);
        }
    }
}
