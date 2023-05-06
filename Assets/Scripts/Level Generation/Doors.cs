using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class MoveRoom : UnityEvent<int, string> { }

public class Doors : MonoBehaviour
{
    private LevelNav ln;

    public GameObject rockPrefab;

    public GameObject rock { get; set; }

    public EnemySpawn es;

    public MoveRoom mr;

    public void Init()
    {
        ln = Camera.main.GetComponent<LevelNav>();

        BoxCollider2D boxy = this.GetComponent<BoxCollider2D>();

        if (this.gameObject.transform.parent.name != ln.currentRoom.name)
        {
            rock =
                Instantiate(
                    rockPrefab,
                    ((boxy.bounds.center + this.transform.position) / 2),
                    Quaternion.identity,
                    this.gameObject.transform
                ) as GameObject;
        }
    }

    //A function to remove the rocks in doorways
    public void rockClear()
    {
        Destroy(this.rock);
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        int index;
        //If room is cleared and colliding tag is player
        if (collide.tag == "Player" && es.clear == EnemySpawn.Clear.Cleared)
        {
            //S1: Up, S2: Right, S3: Down, S4:Left
            switch (this.tag)
            {
                case "S1":
                    index = Int32.Parse(this.transform.parent.name);
                    mr.Invoke((index - 10), "S1");
                    break;

                case "S2":
                    index = Int32.Parse(this.transform.parent.name);
                    mr.Invoke((index + 1), "S2");
                    break;
                case "S3":
                    index = Int32.Parse(this.transform.parent.name);
                    mr.Invoke((index + 10), "S3");
                    break;
                case "S4":
                    index = Int32.Parse(this.transform.parent.name);
                    mr.Invoke((index - 1), "S4");
                    break;

                default:
                    print("No valid tag. Check the tags on the colliders.");
                    break;
            }
        }
    }

    //A function for moving between rooms
    //index - index of current room
    //dir - direction of the door to enter
    public void MoveRoom(int index, string dir)
    {
        ln.MoveCamera(index, dir);

        es = ln.currentRoom.GetComponent<EnemySpawn>();
        if (es.clear != EnemySpawn.Clear.Cleared)
        {
            es.spawnEnemies();
            ln.currentRoom.BroadcastMessage("EnemyStart");
        }
    }
}
