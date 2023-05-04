using UnityEngine;
using UnityEngine.Tilemaps;

public class WallCollision : MonoBehaviour
{
    public EnemyChase enemy;

    public float minX, maxX;
    public float minY, maxY;  

    private TilemapCollider2D box; 



    void Awake() {
        box = GetComponent<TilemapCollider2D>();
        getBoundaries(box);
    }
    private void getBoundaries(TilemapCollider2D boxy) {

        minX = boxy.bounds.min.x;
        maxX = boxy.bounds.max.x;

        minY = boxy.bounds.min.y;
        maxY = boxy.bounds.max.y;

    }

    // on collision detection
    private void OnTriggerEnter2D(Collider2D collide)
    {
        // holds the direction of the wall that was collided with (1 = Up, 2 = Right, 3 = Down, 4 = Left)
        int dir = 0;

        enemy = collide.GetComponent<EnemyChase>();

        if (collide.CompareTag("Enemy"))
        {

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
        }
    }
}
