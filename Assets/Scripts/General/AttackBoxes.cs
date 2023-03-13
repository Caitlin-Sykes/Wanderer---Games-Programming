using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBoxes : MonoBehaviour
{

    // Initialises game box to corresponding childbox
    public GameObject initGameBox(int dir) {
        
        switch(dir) {
            case 1:
                return transform.GetChild(0).gameObject;
            case 2:
                return transform.GetChild(1).gameObject;
            case 3:
                return transform.GetChild(2).gameObject;
            case 4:
                return transform.GetChild(3).gameObject;
            default:
               print("It should fit into one of these cases. Check the code. Returning child 0 as default");
               return transform.GetChild(0).gameObject;
        
        }       
    }
}
