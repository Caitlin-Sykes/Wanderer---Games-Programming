using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // public EnemyChase ec; //instance of EnemyChase class
    public Animator anim;

    private float gradient; //a variable to hold the gradientnitude

    // Sets the animation movement
    public void animationMovement(Vector2 player, Vector3 enemy) {

        gradient = (calcGradient(player, enemy));
        print("gradient" + gradient);
        anim.SetBool("is_moving", false);
        anim.SetFloat("vertical", 0);
        anim.SetFloat("horizontal", 0);

        // If the enemy x is greater than player x and same for y, then move diagonal down left
        if (enemy.x > player.x & enemy.y > player.y) {
            if (gradient >= 0.25 & gradient <= 0.75) {
                anim.SetFloat("vertical", -1);
                anim.SetFloat("horizontal", -1);
                anim.SetBool("is_moving", true);
                print("moving diagonal down left");
            }

            else {
                anim.SetFloat("vertical", 0);
                anim.SetFloat("horizontal", -1);
                anim.SetBool("is_moving", true);
                print("moving left");
            }

        }

        else if (enemy.x < player.x & enemy.y < player.y)
        {
            if (gradient >= 0.25 & gradient <= 0.75)
            {
                anim.SetFloat("vertical", 1);
                anim.SetFloat("horizontal", 1);
                anim.SetBool("is_moving", true);
                print("moving diagonal right-up");
            }

            else
            {
                anim.SetFloat("vertical", 0);
                anim.SetFloat("horizontal", 1);
                anim.SetBool("is_moving", true);
                print("moving right");
            }

        }

        else if (enemy.x > player.x & enemy.y < player.y)
        {
            if (gradient >= -0.25 & gradient <= -0.75)
            {
                anim.SetFloat("vertical", 1);
                anim.SetFloat("horizontal", -1);
                anim.SetBool("is_moving", true);
                print("moving diagonal left up");
            }

            else
            {
                anim.SetFloat("vertical", 1);
                anim.SetFloat("horizontal", 0);
                anim.SetBool("is_moving", true);
                print("moving up");
            }

        }

        else if (enemy.x < player.x & enemy.y > player.y)
        {
            if (gradient >= -0.25 & gradient <= -0.75)
            {
                anim.SetFloat("vertical", -1);
                anim.SetFloat("horizontal", 1);
                anim.SetBool("is_moving", true);
                print("moving diagonal right down");
            }

            else
            {
                anim.SetFloat("vertical", -1);
                anim.SetFloat("horizontal", 0);
                anim.SetBool("is_moving", true);
                print("moving down");
            }

        }
       
    }


    private float calcGradient(Vector2 player, Vector3 enemy) {
        return (enemy.y - player.y) / (enemy.x - player.x);
    }
}