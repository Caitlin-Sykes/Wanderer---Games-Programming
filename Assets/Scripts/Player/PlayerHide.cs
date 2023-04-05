using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{

    public PlayerMovement pmScript;
    public PlayerAttacks pa;
    // Start is called before the first frame update
    public void hideAttack(int dir) { 
        if (pmScript.state == PlayerMovement.State.Hiding) {
            StartCoroutine(pa.mainAttack(dir, 6));
            this.enabled = false;

        }
    }
}
