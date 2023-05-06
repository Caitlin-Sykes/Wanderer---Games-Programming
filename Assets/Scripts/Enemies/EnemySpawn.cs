
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] spawnPoints; //spawn points for enemies

    public GameObject[] enemyTypes; //enemy types
    private Doors[] doors;

    private bool initEnemy = false;

    public enum Clear
    {
        Cleared,
        NotCleared
    };

    public Clear clear = Clear.NotCleared;

    void Start() {
        doors = this.gameObject.GetComponentsInChildren<Doors>();
    }
    void Update() {

        try {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if ((enemies.Length == 0) && (this.initEnemy == true) && (this.clear == Clear.NotCleared)) {
            this.clear = Clear.Cleared;
           
            foreach (Doors doorway in doors) {
                doorway.rockClear();
            }
        }

        }

        catch {
            print("beep");
        }
    }


    //A function to randomly generate how many enemies to spawn
    private int noOfSpawns() {
        return Random.Range(0, 6);
    }

    //A function to randomly pick an enemy type
    private GameObject getEnemyType() {
        return enemyTypes[Random.Range(0, enemyTypes.Length)];
    }

    //A function to randomly pick an enemy spawn location
    private Vector3 getEnemySpawnLoc() {
        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }

    //Spawn enemies in room
    public void spawnEnemies() {

        this.initEnemy = true;
        int noOfEnems = noOfSpawns();
        
        //For the number of enemies to spawn
        for (int i = 0; i <= noOfEnems; i++) {
            GameObject enem = Instantiate(getEnemyType(), getEnemySpawnLoc(), Quaternion.identity, this.gameObject.transform) as GameObject;
        }
    }
}
