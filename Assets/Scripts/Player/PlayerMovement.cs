using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 direction;
    Animator anim; // instance of animator
    public float speed;
    Rigidbody2D rb;
    public PlayerAttacks pa;

    public PlayerHide ph;

    public enum AttackDir
    {
        N,
        E,
        S,
        W,
        NA
    }; //(NA = not attacking)

    public AttackDir attackDir = AttackDir.NA;

    public enum State
    {
        Moving,
        Hiding,
        Idle
    };

    public State state = State.Idle;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        getPlayerInput();
        setAnimatorMovement();
    }

    void FixedUpdate()
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    // Sets the animation movements
    private void setAnimatorMovement()
    {
        anim.SetFloat("vertical", direction.x);
        anim.SetFloat("horizontal", direction.y);

        if (state == State.Moving)
        {
            anim.SetBool("is_moving", true);
            anim.SetBool("hide", false);
        }
        else if (state == State.Hiding)
        {
            anim.SetBool("hide", true);
            anim.SetBool("is_moving", false);
        }
        else if (state == State.Idle)
        {
            anim.SetBool("is_moving", false);
            anim.SetBool("hide", false);
        }
        else
        {
            anim.SetBool("is_moving", true);
            anim.SetBool("hide", false);
            state = State.Moving;
        }
    }

    // Controls the attack animation
    public void attackAnim()
    {
        convertCTI();

        if (state == State.Moving || state == State.Idle)
        {
            anim.SetTrigger("Attack");
            state = State.Moving;
        }
        else if (state == State.Hiding)
        {
            state = State.Moving;
            anim.SetTrigger("unhideAttack");
        }
    }

    // Gets the player's input
    private void getPlayerInput()
    {
        direction = Vector2.zero;

        if (state != State.Hiding)
        {
            state = State.Idle;
        }

        // Movement Keys (WASD)
        // If statements not else if so can move diagonal
        // Move up
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            checkHide(false);
        }
        // move left
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            checkHide(false);
        }

        //move down
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            checkHide(false);
        }

        // move right
        if ((Input.GetKey(KeyCode.D)))
        {
            direction += Vector2.right;
            checkHide(false);
        }

        // Attack Controls
        // Up, Down, Left, Right Arrows
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            attackDir = AttackDir.N;
            checkHide(true);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            attackDir = AttackDir.S;
            checkHide(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            attackDir = AttackDir.W;
            checkHide(true);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            attackDir = AttackDir.E;
            checkHide(true);
        }

        // Hide Controls
        // press h to hide
        if (Input.GetKeyDown(KeyCode.H))
        {
            state = State.Hiding;
        }
    }

    //checkHide is used to call different functions depending on whether Moss is hiding or not.
    private void checkHide(bool attack)
    {
        if (attack == false)
        {
            if (state == State.Hiding)
            {
                anim.SetTrigger("unhide");
            }

            state = State.Moving;
        }
        else
        {
            int dir = convertCTI();
            // If attack is true and state is hiding
            if (state == State.Hiding)
            {
                anim.SetBool("hide", false);
                anim.SetTrigger("unhideAttack");
                state = State.Moving;
                ph.hideAttack(dir);
            }
            else
            {
                StartCoroutine(pa.mainAttack(dir, 3));
                state = State.Moving;
            }
        }
    }

    //Function to convert character to integer for the directions
    private int convertCTI()
    {
        switch (attackDir)
        {
            case AttackDir.N:
                anim.SetInteger("attackDir", 1);
                return 1;
            case AttackDir.E:
                anim.SetInteger("attackDir", 2);
                return 2;
            case AttackDir.S:
                anim.SetInteger("attackDir", 3);
                return 3;
            case AttackDir.W:
                anim.SetInteger("attackDir", 4);
                return 4;
            default:
                break;
        }

        return -1;
    }
}
