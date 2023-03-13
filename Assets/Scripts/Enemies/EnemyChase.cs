using System.Collections;
using UnityEngine;

public class EnemyChase : MonoBehaviour

{
    public Transform player; // Variable to hold the position of the enemy

    public EnemyMovement em;  // Variable to hold an instance of EnemyMovement

    private Vector3 enemy; // Variable to hold the position of the enemy

    public float speed;

    private int damage {get; set;} = 5;

    // Called when "EnemyStart" is broadcasted
    private IEnumerator EnemyStart() {

        Health health = this.GetComponent<Health>();

        while (health.health > 0) {
            // Chases
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Calls animationMovement function from EnemyMovement
            em.animationMovement(player.position, transform.position);


            yield return null;
            
        }
        
    }

    // Gets the collisions
    private void OnTriggerEnter2D(Collider2D collide)
    {

        int attackDir = em.getAttackDir(player, transform.position);

        // print(player.transform.position);
        // print("enem" + transform.position);
    
        if (attackDir != -1) {

            // If tag is enemy and it has health
            if (collide.CompareTag("Player") && collide.GetComponent<Health>() != null)
            {
    
                // Initialises instance
                Health healthVar = collide.GetComponent<Health>();
            
                // Decrements health
                healthVar.healthDecrement(damage);

            }
        }
    }



    // // Recieves direction and moves the enemy accordingly
    // // Called from WallCollision 
    public void bounce(int dir) {

        // If dir is one (meaning top wall was collided) then move down
        if (dir == 1) {

            // print("UpWall");
            enemy.y -= 2;

            transform.position += (enemy * speed * Time.deltaTime);

        }

        // If dir is two (meaning right wall was collided) then move left
        else if (dir == 2)
        {
            // print("RightWall");
            enemy.y -= 2;

            // moves rigidbody
            transform.position += (enemy * speed * Time.deltaTime);

        }

        // If dir is three (meaning bottom wall was collided) then move up
        else if (dir == 3)
        {
            enemy.y += 2;

            // print("BottomWall");

            // moves rigidbody
            transform.position += (enemy * speed * Time.deltaTime);
        }

        // If dir is four (meaning left wall was collided) then move right
        else if (dir == 4)
        {
            // print("LeftWall");
            enemy.x += 2;
            // moves rigidbody
            transform.position += (enemy * speed * Time.deltaTime);

        }
    }

    // TODO: fix chasing algorithm so it chases
    // TODO: fix bouncy
    // TODO: make better




}




// enemy types: multiplies like rabbits
// aggressive smaller animals
// quick deer
// sneaky birds.

