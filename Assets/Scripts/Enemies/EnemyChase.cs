using System.Collections;
using UnityEngine;

public class EnemyChase : EnemyMovement

{
    private GameObject player; // Variable to hold the position of the player

    private PlayerMovement pmScript;

    private Vector3 enemy; // Variable to hold the position of the enemy

    public float speed;

    private Vector3 xy; //for random coords

    private int damage { get; set; } = 5;

    // holds attack damage
    private bool invinceFrame { get; set; } = false;

    private WallCollision wc;

    void Awake() {
        wc = this.transform.parent.gameObject.GetComponentInChildren<WallCollision>();
    }

    // Called when "EnemyStart" is broadcasted
    //A function to start enemy ai
    private IEnumerator EnemyStart()
    {

        yield return new WaitForSeconds(1);

        Health health = this.gameObject.GetComponent<Health>();
        randomCoOrds();
        float duration = 5;
        
        //While health is not 0
        while (health.health > 0)
        {
             player = GameObject.FindGameObjectWithTag("Player");
             pmScript = player.GetComponent<PlayerMovement>();

            if (pmScript.state == PlayerMovement.State.Moving || pmScript.state == PlayerMovement.State.Idle && player != null )
            {
                // Chases
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (speed * Time.deltaTime));

                // Calls animationMovement function from EnemyMovement
                 animationMovement(player.transform.position, this.transform.position);


                yield return null;
            }


            //Else gens random coords
            else
            {
                duration -= Time.deltaTime;
                if (duration % 5 ==0) {
                    randomCoOrds();
                    duration = 5;
                                    }

                else if ((transform.position) == xy) {
                    randomCoOrds();

                }

                transform.position = Vector2.MoveTowards(transform.position, xy, (speed * Time.deltaTime));
                // Calls animationMovement function from EnemyMovement
                animationMovement(xy, transform.position);
                yield return null;

            }
        }
    }

    // Gets the collisions
    private IEnumerator OnTriggerEnter2D(Collider2D collide)
    {
        pmScript = collide.GetComponent<PlayerMovement>();
        print("collide " + collide);

        // If tag is enemy and it has health
        if (collide.CompareTag("Player") && collide.GetComponent<Health>() != null)
        {
            if (getAttackDir(collide.transform, transform.position) != -1 && invinceFrame != true)
            {
                // Initialises instance
                Health healthVar = collide.GetComponent<Health>();

                // Decrements health
                healthVar.healthDecrement(damage);


                if (pmScript.state == PlayerMovement.State.Hiding)
                {
                    pmScript.state = PlayerMovement.State.Moving;
                }

                invinceFrame = true;
                yield return new WaitForSeconds(2);
                invinceFrame = false;

            

            }
        }

        else
        {
            randomCoOrds();
            yield return new WaitForSeconds(2f);
        }
    }



    // // Recieves direction and moves the enemy accordingly
    // // Called from WallCollision 
    //@param dir as int - direction
    public void bounce(int dir)
    {

        // If dir is one (meaning top wall was collided) then move down
        if (dir == 1)
        {
            enemy.y -= 2;
            transform.position += (enemy * speed * Time.deltaTime);
        }

        // If dir is two (meaning right wall was collided) then move left
        else if (dir == 2)
        {
            enemy.y -= 2;
            transform.position += (enemy * speed * Time.deltaTime);
        }

        // If dir is three (meaning bottom wall was collided) then move up
        else if (dir == 3)
        {
            enemy.y += 2;
            transform.position += (enemy * speed * Time.deltaTime);
        }

        // If dir is four (meaning left wall was collided) then move right
        else if (dir == 4)
        {
            enemy.x += 2;
            transform.position += (enemy * speed * Time.deltaTime);
        }
    }







private void randomCoOrds() {
    xy = new Vector2(Random.Range(wc.minX, wc.maxX), Random.Range(wc.minY, wc.maxY));
}
}

//TODO: more enemy types
//  enemy types: multiplies like rabbits
// aggressive smaller animals
// quick deer
// sneaky birds.

