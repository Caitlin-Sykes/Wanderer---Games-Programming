using UnityEngine;

public class PlayerHide : MonoBehaviour
{

    public PlayerMovement pmScript;
    public PlayerAttacks pa;
    
    //A function to hide the attack
    //dir - direction of attack
    public void hideAttack(int dir) { 
        if (pmScript.state == PlayerMovement.State.Hiding) {
            StartCoroutine(pa.mainAttack(dir, 6));
            this.enabled = false;

        }
    }
}
