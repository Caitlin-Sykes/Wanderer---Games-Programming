using UnityEngine;
using System.Collections;
public class PlayerAttacks : AttackBoxes

{
    public GameObject attackBox = default;  // To hold the attack box which determines when things are hit

    public PlayerMovement pm;
    
    public bool delay { get; set; } = false; // holds attacking delay

    public int damage { get; set; } = 3; // holds attacking delay

    // main attack function
    public IEnumerator mainAttack(int attackDir, int dmg) {

        if (delay == false)
        {
            this.damage = dmg;

            delay = true;

            // Inits attack box
            attackBox = initGameBox(attackDir);

            pm.attackAnim();

            // Sets the collision box to true
            attackBox.SetActive(true);

            yield return new WaitForSeconds(1f);

            if (attackBox.activeInHierarchy == true) {
                attackBox.SetActive(false);

            }
            delay = false;
            yield return new WaitForSeconds(1f);
        }
    }



    
}
