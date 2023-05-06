using UnityEngine;

public class MainDoor : MonoBehaviour
{
    Introduction intro;

    public ChangeScene cs;

    void Awake()
    {
        try
        {

                GameObject go = GameObject.Find("EventSystem");
                intro = go.GetComponent<Introduction>();
                cs = go.GetComponent<ChangeScene>();
        }
        catch
        {
            print("Can't find game object");
            GameObject cam = Camera.main.gameObject;
            cs = cam.GetComponent<ChangeScene>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collide)
    {
        print(collide);
        try
        {
            if (
                intro.dialoguePassed == true
                && collide.CompareTag("Player")
                && this.gameObject.CompareTag("MainDoor")
            )
            {
                cs.loadScene("1");
            }
        }
        catch
        {
            if (collide.CompareTag("Player") && this.gameObject.CompareTag("Escape"))
            {
                cs.loadScene("Boss");
            }
            else if (collide.CompareTag("Player") && this.gameObject.CompareTag("MainDoor2"))
            {
                cs.loadScene("Done");
            }
        }
    }
}
