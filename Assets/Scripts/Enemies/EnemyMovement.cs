using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Animator anim;

    private float gradient; //a variable to hold the gradient

    // Sets the animation movement
    //@param Vector2 player - position of player
    //@param Vector3 enemy - position of enemy
    public void animationMovement(Vector2 player, Vector3 enemy)
    {

        gradient = (calcGradient(player, enemy));
        setAnimator(0, 0, false);


        // A series of if statements to determine what animation should play
        if (enemy.x > player.x & enemy.y > player.y)
        {
            if (gradient >= 0.20 & gradient <= 0.80)
            {
                setAnimator(0, -1, true);
            }

            else
            {
                setAnimator(-1, -1, true);
            }

        }

        else if (enemy.x < player.x & enemy.y < player.y)
        {
            if (gradient >= 0.20 & gradient <= 0.80)
            {
                setAnimator(0, 1, true);
            }

            else
            {
                setAnimator(1, 1, true);
            }

        }

        else if (enemy.x > player.x & enemy.y < player.y)
        {
            if (gradient >= -0.20 & gradient <= -0.20)
            {
                setAnimator(1, 0, true);
            }

            else
            {
                setAnimator(1, -1, true);
            }

        }

        else if (enemy.x < player.x & enemy.y > player.y)
        {
            if (gradient >= -0.20 & gradient <= -0.80)
            {
                setAnimator(-1, 0, true);
            }

            else
            {
                setAnimator(-1, 1, true);
            }

        }

    }


    // Calcs gradient
    //@param Vector2 player - position of player
    //@param Vector3 enemy - position of enemy
    private float calcGradient(Vector2 player, Vector3 enemy)
    {
        return (enemy.y - player.y) / (enemy.x - player.x);
    }

    // Sets animatorPosition
    //@param int v - vertical
    //@param int h - horizontal
    //@param int move - moving bool
    private void setAnimator(int v, int h, bool move)
    {
        anim.SetBool("is_moving", move);
        anim.SetFloat("vertical", v);
        anim.SetFloat("horizontal", h);

    }


    // Calculates the attacking direction
    //@param Transform player - transform of player
    //@param Vector2 enemy - position of enemy
    public int getAttackDir(Transform player, Vector2 enemy)
    {
        if ((enemy.x >= (player.position.x - 2)) || (enemy.x <= (player.position.x + 2)))
        {
            if (enemy.x < player.position.x)
            {
                return 2;
            }

            else if (enemy.x > player.position.x)
            {
                return 4;
            }

            else
            {
                return -1;
            }

        }

        else if ((enemy.y >= (player.position.y - 2)) || (enemy.y <= (player.position.y + 2)))
        {
            if (enemy.y < player.position.y)
            {
                return 1;
            }

            else if (enemy.y > player.position.y)
            {
                return 3;
            }

            else
            {
                return -1;
            }

        }

        else
        {
            return -1;
        }
    }


}
