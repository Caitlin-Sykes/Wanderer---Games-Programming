using UnityEngine;
using System.Collections;
public class PlayerAttacks : MonoBehaviour

{
    // To hold the attack box which determines when things are hit
    private GameObject attackBox = default;

    public PlayerMovement pm;

    // Instance of AttackBoxes
    public AttackBoxes ab;

    // holds attack damage
    private int damage {get; set;} = 2;

    // holds attacking delay
    public bool delay { get; set; } = false;

    // instance of health
    public Health health;


    // main attack function
    public IEnumerator mainAttack(int attackDir) {

        if (delay == false) {
            
            print("can enter");
            delay = true;

            // Inits attack box
            attackBox = ab.initGameBox(attackDir);

            // Sets damage to three
            damage = 3;

            // Sets the collision box to true
            attackBox.SetActive(true);

            pm.attackAnim();

            // Waits for two seconds
            yield return new WaitForSeconds(1.0f);

            // Disables it again
            attackBox.SetActive(false);

            // Stops spamming of the attacks.
            yield return new WaitForSeconds(1f);

            delay = false;
        }

        else {
            print("cant enter");
        }
    }

    // Gets the collisions
    private void OnTriggerEnter2D(Collider2D collide)
    {
        // If tag is enemy and it has health
        if (collide.CompareTag("Enemy") && collide.GetComponent<Health>() != null)
        {
            // Initialises instance
            Health healthVar = collide.GetComponent<Health>();
            // Decrements health
            healthVar.healthDecrement(damage);
            
        }
    }



    
}
