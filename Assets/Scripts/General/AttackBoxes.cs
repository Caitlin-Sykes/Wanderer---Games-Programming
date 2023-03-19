using System.Collections;
using UnityEngine;

public class AttackBoxes : MonoBehaviour
{
    private Health healthVar;
    private PlayerAttacks pa;

    void Start() {
        pa = GetComponent<PlayerAttacks>();
    }

    // Initialises game box to corresponding childbox
    public GameObject initGameBox(int dir) {
        switch(dir) {
            case 1:
                return transform.GetChild(0).gameObject;
            case 2:
                return transform.GetChild(1).gameObject;
            case 3:
                return transform.GetChild(2).gameObject;
            case 4:
                return transform.GetChild(3).gameObject;
            default:
               print("It should fit into one of these cases. Check the code. Returning child 0 as default");
               return transform.GetChild(0).gameObject;
        
        }       
    }

    // Gets the collisions
    private void OnTriggerEnter2D(Collider2D collide)
    {

            // If tag is enemy and it has health
            if (collide.transform.CompareTag("Enemy") & collide.GetComponent<Health>() != null & transform.CompareTag("Player") != false)
            {
                healthVar = collide.GetComponent<Health>();
                healthVar.healthDecrement(pa.damage); //problematic line that throws errors yet works??

                if (collide.GetComponent<Rigidbody2D>() != null)
                {
                    Vector2 collision = ((collide.transform.position - transform.position));

                    // an extra line of code because it randomly breaks if I do it all in one line \_o_/
                    collision = collision.normalized * 4;

                    collide.GetComponent<Rigidbody2D>().AddForce(collision, ForceMode2D.Impulse);
                    StartCoroutine(cancelForce(collide.GetComponent<Rigidbody2D>()));
                }
            }

            else
            {
                return;
            }
        

        
    }
    
    // A function to cancel the force after a certain amount of time
    private IEnumerator cancelForce(Rigidbody2D target)
    {
        yield return new WaitForSeconds(1);
        target.velocity = Vector2.zero;
    }
    
}
