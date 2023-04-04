using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.AI;


public class Health : MonoBehaviour
{
    [SerializeField] public int health;

    //Max Health 
    public int MAXHEALTH = 100;

    // instance of animator
    public Animator anim;

    // Event
    public UnityEvent playerHit;

    void start()
    {
        this.health = MAXHEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        // If the health is zero and the tag is player, then game over
        if (health <= 0 && this.CompareTag("Player"))
        {
            SendMessageUpwards("gameOver");

        }

        // If health is 0 and tag is enemy, destroys game object and logs to console
        else if (health <= 0 && this.CompareTag("Enemy"))
        {
            // Starts couroutine
            StartCoroutine(death());
        }
    }

    // a function to decrement health
    public void healthDecrement(int damage)
    {
        if (damage > 0)
        {
            this.health -= damage;
            if (this.CompareTag("Player"))
            {
                playerHit.Invoke();

            }
        }
    }

    // Destroys the game object
    IEnumerator death()
    {
        anim.SetTrigger("EnemyDeath");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}

