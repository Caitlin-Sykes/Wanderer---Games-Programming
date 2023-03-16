
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //direction
    private Vector2 direction;

    // instance of animator
     Animator anim;

    // Speed 
    public float speed;

    // is_moving variable
    private bool is_moving;

    // hide variable
    // holds attack damage
    public bool hide {get; set;} = false;

    // RigidBody
    public Rigidbody2D rb;

    // Attack Script
    public PlayerAttacks pa;

    // Variable to determine attacking direction (1 - N, 2-E, 3-S, 4-W)
    public int attackDir;

    private bool attack;


    void Start() {
        anim = GetComponent<Animator>();
        attack = false;
    }

    // Update is called once per frame
    void Update() {
        getPlayerInput();
        setAnimatorMovement();
    }

    void FixedUpdate() {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
    private void setAnimatorMovement()
    {
        anim.SetFloat("vertical", direction.x);
        anim.SetFloat("horizontal", direction.y);
        anim.SetBool("is_moving", is_moving);
        anim.SetBool("hide", hide);
    }

    public void attackAnim() {
        if (attack == true)
        {
            attack = false;
            anim.SetInteger("attackDir", attackDir);
            anim.SetTrigger("Attack");
        }
        
    }

    // Gets the player's input
    private void getPlayerInput() {

        direction = Vector2.zero;
        is_moving = false;

        // Movement Keys (WASD)
        // If statements not else if so can move diagonal
        // Move up
        if (Input.GetKey(KeyCode.W)) {
            direction += Vector2.up;
            is_moving = true;
            hide = false;

        }
        // move left
        if (Input.GetKey(KeyCode.A)) {
            direction += Vector2.left;
            is_moving = true;
            hide = false;
        }

        //move down
        if (Input.GetKey(KeyCode.S)) {

            direction += Vector2.down;
            is_moving = true;
            hide = false;
        }

        // move right
        if ((Input.GetKey(KeyCode.D))) {
            direction += Vector2.right;
            is_moving = true;
            hide = false;
        }

        // Attack Controls
        // Up, Down, Left, Right Arrows
        if (Input.GetKeyDown(KeyCode.UpArrow)) {

            attackDir = 1;
            attack = true;
            StartCoroutine(pa.mainAttack(attackDir));
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow)) {

            attackDir = 3;
            attack = true;
            StartCoroutine(pa.mainAttack(attackDir)); ;
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            attackDir = 4;
            attack = true;
            StartCoroutine(pa.mainAttack(attackDir));
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            attackDir = 2;
            attack = true;
            StartCoroutine(pa.mainAttack(attackDir));
        }

        // Hide Controls
        // press h to hide
        if (Input.GetKey(KeyCode.H))
        {
            hide = true;
        }
    }

    
    

    
}
