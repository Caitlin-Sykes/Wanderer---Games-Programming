using UnityEngine;

public class Health : MonoBehaviour
{
    // Player Health
    [SerializeField] private int health;

    //Instance of Change Scene
    public ChangeScene changeScene;

    //Max Health
    public int MAXHEALTH = 100;

    // instance of animator
    public Animator anim;

    void start() {
        this.health = MAXHEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        // If the health is zero and the tag is player, then game over
        if (health < 0 && this.CompareTag("Player")) {
            changeScene.loadScene("GameOver");
        }

        // If health is 0 and tag is enemy, destroys game object and logs to console
        else if (health < 0 && this.CompareTag("Enemy")) {
            anim.SetTrigger("EnemyDeath");
            Destroy(this.gameObject);
            print("You are dead, so very dead, enemy dead");
        }
    }

    // a function to decrement health
    public void healthDecrement(int damage) {
        if (damage > 0) {
            this.health -= damage;
            print(health);
        }
    }
}

// TODO: add some AI for enemies
// TODO: fix shit.
// TODO: back up working code to github

