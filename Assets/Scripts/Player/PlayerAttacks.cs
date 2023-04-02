using UnityEngine;
using System.Collections;
public class PlayerAttacks : AttackBoxes

{
    private GameObject attackBox = default;  // To hold the attack box which determines when things are hit

    public PlayerMovement pm;
    public int damage {get; set;} /// holds attack damage
    public bool delay { get; set; } = false; // holds attacking delay

    // main attack function
    public IEnumerator mainAttack(int attackDir, int dmg) {

        damage = dmg;
        if (delay == false)
        {
            print("in the attacking loop");

            delay = true;

            // Inits attack box
            attackBox = initGameBox(attackDir);

            pm.attackAnim();

            // Sets the collision box to true
            attackBox.SetActive(true);

            yield return new WaitForSeconds(1f);
            attackBox.SetActive(false);
            delay = false;
            yield return new WaitForSeconds(1f);
        }
    }



    
}
