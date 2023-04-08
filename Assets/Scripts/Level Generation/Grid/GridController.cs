using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    private int row = 10;
    private int col = 10;

    public GameObject tile;
    void Start()
    {
        genGrid();
    }


    // Update is called once per frame
    private void genGrid()
    {

        // for (int e = 0; e < 10; e++)
        // {
            for (int r = 0; r < row; r++)
                for (int c = 0; c < col; c++)
                {
                    GameObject temp = Instantiate(tile, transform);
                    temp.transform.position = new Vector2(c * 10, r * -10);
                    // if (c > 1) {
                    //     // temp.transform.position = new Vector2(c*10,r*-10);
                    // }



                }
        // }
        Destroy(tile);
    }



}

