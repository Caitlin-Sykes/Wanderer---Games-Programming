using UnityEngine;
using System.Collections;
using System;
public class PlayerAttacks : AttackBoxes

{
    // To hold the attack box which determines when things are hit
    private GameObject attackBox = default;

    public PlayerMovement pm;

    // holds attack damage
    public int damage {get; set;} = 2;

    // holds attacking delay
    public bool delay { get; set; } = false;

    // instance of health
    public Health health;


    // main attack function
    public IEnumerator mainAttack(int attackDir) {

        if (delay == false)
        {

            delay = true;

            // Inits attack box
            attackBox = initGameBox(attackDir);

            // Sets the collision box to true
            attackBox.SetActive(true);

            pm.attackAnim();

            // Waits for two seconds
            yield return new WaitForSeconds(1f);

            // Disables it again
            attackBox.SetActive(false);

            delay = false;

            // Stops spamming of the attacks.
            yield return new WaitForSeconds(1f);
        }
    }



    
}
