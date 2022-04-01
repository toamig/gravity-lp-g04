using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject[] holes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            holes = GameObject.FindGameObjectsWithTag("BlackHole");
            foreach (GameObject hole in holes)
            {
                Destroy(hole);
            }
            GameEvents.instance.ReachGoal();
        }
    }
}
