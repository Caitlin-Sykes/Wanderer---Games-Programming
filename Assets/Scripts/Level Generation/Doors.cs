using System;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private LevelNav ln;

    public GameObject rockPrefab;

    private GameObject rockItem;

    public enum Clear
    {
        Cleared,
        NotCleared
    };

    public Clear clear = Clear.Cleared;

    void Start()
    {
        ln = Camera.main.GetComponent<LevelNav>();
        
        BoxCollider2D boxy = this.GetComponent<BoxCollider2D>();

        rockItem = Instantiate(rockPrefab, boxy.bounds.center, Quaternion.identity);
    }


    //A function to remove the rocks in doorways
    public void rockClear() {
        Destroy(rockItem);
    }


    private void OnTriggerEnter2D(Collider2D collide)
    {
        int index;

        //If room is cleared and colliding tag is player
        if (collide.tag == "Player" && clear == Clear.Cleared)
        {
            //S1: Up, S2: Right, S3: Down, S4:Left
            switch (this.tag)
            {
                case "S1":
                    index = Int32.Parse(this.transform.parent.name);
                    print("b4 " + index);
                    ln.MoveCamera((index - 10), "S1");
                    break;

                case "S2":
                    index = Int32.Parse(this.transform.parent.name);
                    print("b4 " + index);
                    ln.MoveCamera((index + 1), "S2");
                    break;
                case "S3":
                    index = Int32.Parse(this.transform.parent.name);
                    Destroy(GameObject.Find("Player"));
                    ln.MoveCamera((index + 10), "S3");
                    break;
                case "S4":
                    index = Int32.Parse(this.transform.parent.name);
                    print("b4 " + index);
                    ln.MoveCamera((index - 1), "S4");
                    break;

                default:
                    print("sus");
                    break;
            }
        }
    }
}
