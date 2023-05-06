using UnityEngine;
using System.Collections;
using UnityEngine.Events;


public class Health : MonoBehaviour
{
    [SerializeField] public int health;

    //Max Health 
    public int MAXHEALTH = 100;

    // instance of animator
    public Animator anim;

    // Event
    public UnityEvent hit;

    OnScene os;

    void Awake() {
        os = Camera.main.GetComponent<OnScene>();
        this.health = MAXHEALTH;
    }


    // Update is called once per frame
    void Update()
    {
        // If the health is zero and the tag is player, then game over
        if (health <= 0 && this.CompareTag("Player"))
        {
            os.gameOver();

        }

        // If health is 0 and tag is enemy, destroys game object and logs to console
        else if (health <= 0 && this.CompareTag("Enemy"))
        {
            // Starts couroutine
            StartCoroutine(death());
        }
    }

    // a function to decrement health
    //@param: damage - the damage to be taken
    public void healthDecrement(int damage)
    {
        if (damage > 0)
        {
            this.health -= damage;
            hit.Invoke();
        }
    }

    // Destroys the game object on death and plays the animation
    IEnumerator death()
    {
        anim.SetTrigger("EnemyDeath");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    //A function to set the health
     public void setHealth(int health)
    {
            this.health = health;
            hit.Invoke();
        }
    

}

