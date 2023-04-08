using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridController : MonoBehaviour
{
    public List<GameObject> rooms = new List<GameObject>();
    public GameObject tile;

    public UnityEvent startGen;

    void Start() {
        startGen.Invoke();
    }
 
    // Generates an empty grid and adds to rooms list
    public void genGrid()
    {
        for (int r = 0; r < 10; r++)
            for (int c = 0; c < 10; c++)
            {
                GameObject temp = Instantiate(tile, transform);
                string t = (r.ToString() + c.ToString());
                temp.name = t;
                temp.transform.position = new Vector2(c * 10, r * -10);
                rooms.Add(temp);
            }
        
    }



}

