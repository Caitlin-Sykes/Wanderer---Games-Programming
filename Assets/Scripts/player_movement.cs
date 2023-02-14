using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
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
    private bool hide;


    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        getPlayerInput();
        MovePlayer();
    }

    private void setAnimatorMovement()
    {
        anim.SetFloat("vertical", direction.x);
        anim.SetFloat("horizontal", direction.y);
        anim.SetBool("is_moving", is_moving);
        anim.SetBool("hide", hide);
    }

    private void MovePlayer() {
        // Delta time stops you going faster if your frame rate is high
        transform.Translate(direction * speed * Time.deltaTime);
        setAnimatorMovement();
    }

    // Gets the player's input
    private void getPlayerInput() {

        direction = Vector2.zero;
        is_moving = false;

        // Movement Keys (WASD, or the arrow keys)
        // If statements not else if so can move diagonal
        // Move up
        if ((Input.GetKey(KeyCode.W)) || (Input.GetKey(KeyCode.UpArrow))) {
            direction += Vector2.up;
            is_moving = true;
            hide = false;

        }
        // move left
        if ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.LeftArrow))) {
            direction += Vector2.left;
            is_moving = true;
            hide = false;
        }

        //move down
        if ((Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.DownArrow))) {

            direction += Vector2.down;
            is_moving = true;
            hide = false;
        }

        // hold down to hide
        if (Input.GetKey(KeyCode.H))
        {
            hide = true;
        }

        // move right
        if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.RightArrow))) {
            direction += Vector2.right;
            is_moving = true;
            hide = false;
        }
    }

    
}
