using System;
using UnityEngine;

public class Doors : MonoBehaviour
{
    private LevelNav ln;

    public enum Clear
    {
        Cleared,
        NotCleared
    };

    public Clear clear = Clear.Cleared;

    void Start()
    {
        ln = Camera.main.GetComponent<LevelNav>();
        print(ln);
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        int index;
        print("tagi" + this.tag);

        if (collide.tag == "Player")
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
                    print("b4 " + index);
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
