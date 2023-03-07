using System.Collections;
using UnityEngine;

public class EnemyChase : MonoBehaviour

{
    // Variable to hold the position of the enemy
    public Transform player;

    // Variable to hold the position of the enemy
    private Vector3 enemy;

    // Variable to hold the speed of the enemy
    public float speed;

    // Update is called once per frame
    void Update()
    {

        // Variable to store the current position of the player
    


        //move towards the player
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);


    }

    // // Recieves direction and moves the enemy accordingly
    // // Called from WallCollision 
    public void bounce(int dir) {

        // If dir is one (meaning top wall was collided) then move down
        if (dir == 1) {

            // print("UpWall");
            enemy.y -= 1;

            transform.position += (enemy * speed * Time.deltaTime);

        }

        // If dir is two (meaning right wall was collided) then move left
        else if (dir == 2)
        {
            // print("RightWall");
            enemy.y -= 1;

            // moves rigidbody
            transform.position += (enemy * speed * Time.deltaTime);

        }

        // If dir is three (meaning bottom wall was collided) then move up
        else if (dir == 3)
        {
            enemy.y += 1;

            // print("BottomWall");

            // moves rigidbody
            transform.position += (enemy * speed * Time.deltaTime);
        }

        // If dir is four (meaning left wall was collided) then move right
        else if (dir == 4)
        {
            // print("LeftWall");
            enemy.x += 1;
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

