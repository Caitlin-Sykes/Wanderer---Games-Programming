using UnityEngine;
using System.Collections;
using System;
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

             delay = false;

            // Stops spamming of the attacks.
            yield return new WaitForSeconds(1f);
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

            if (collide.GetComponent<Rigidbody2D>() != null) {
                Vector2 collision = ((collide.transform.position - transform.position));

                // an extra line of code because it randomly breaks if I do it all in one line \_o_/
                collision = collision.normalized * 7;

                collide.GetComponent<Rigidbody2D>().AddForce(collision, ForceMode2D.Impulse);
                StartCoroutine(cancelForce(collide.GetComponent<Rigidbody2D>()));
            }

            

        }

        else {
            return;
        }
    }

    private IEnumerator cancelForce(Rigidbody2D target) {

        print("before");
        yield return new WaitForSeconds(1);
        print("after");

        target.velocity = Vector2.zero;
    }



    
}
