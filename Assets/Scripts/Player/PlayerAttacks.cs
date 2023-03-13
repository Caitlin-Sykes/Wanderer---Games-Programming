using UnityEngine;
using System.Collections;
public class PlayerAttacks : MonoBehaviour

{
    // To hold the attack box which determines when things are hit
    private GameObject attackBox = default;

    // Instance of AttackBoxes
    public AttackBoxes ab;

    // holds attack damage
    private int damage {get; set;} = 2;

    // instance of health
    public Health health;


    // main attack function
    public IEnumerator mainAttack(int attackDir) {
        // Inits attack box
        attackBox = ab.initGameBox(attackDir);

        // Sets damage to three
        damage = 3;

        // Sets the collision box to true
        attackBox.SetActive(true);

        // Waits for two seconds
        yield return new WaitForSeconds(1.0f);

        // Disables it again
        attackBox.SetActive(false);

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
