using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNav : MonoBehaviour
{
    
    private Camera cam = Camera.main;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            cam.transform.position = new Vector3(0,-30,-0.01f);

        }
        
    }
}
