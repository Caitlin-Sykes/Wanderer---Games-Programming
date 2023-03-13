using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    public EnemyChase enemy;

    // on collision detection
    private void OnTriggerEnter2D(Collider2D collide)
    {
        // holds the direction of the wall that was collided with (1 = Up, 2 = Right, 3 = Down, 4 = Left)
        int dir = 0;

        if(collide.CompareTag("Enemy")) {

            // If it collides with the Walls and has the tag hitboxUp
        if (collide.CompareTag("AttackBoxU"))
        {
            dir = 1;
            enemy.bounce(dir);
        }

        // If it collides with the Walls and has the tag hitboxDown
        if (collide.CompareTag("AttackBoxD"))
        {
            dir = 3;
            enemy.bounce(dir);
        
        }

        // If it collides with the Walls and has the tag hitboxLeft
        if (collide.CompareTag("AttackBoxL"))
        {
            dir = 4;
            enemy.bounce(dir);
        }

        // If it collides with the Walls and has the tag hitboxRight
        if (collide.CompareTag("AttackBoxR"))
        {
            dir = 2;
            enemy.bounce(dir);
        }
    

    if(collide.CompareTag("Player")) {
        print("BonkityBonkPlayerBonks");

        }
        }
}
}
