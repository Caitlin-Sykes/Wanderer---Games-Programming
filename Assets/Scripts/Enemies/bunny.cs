using UnityEngine;
using System.Collections;

public class bunny : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject canvas;

    public GameObject[] dialogue;
    public bool dialogPassed { get; set; } = false;

    public BoxCollider2D doorBox;

    public GameObject aDialog;

    public bool swi = true;

    void Awake()
    {
        doorBox.enabled = false;
    }

    // Start is called before the first frame update

    void Start()
    {
        StartDialogue();
        movement();
    }

    void Update()
    {
        this.GetComponent<Animator>().SetFloat("h", 0);

        if (
            this.GetComponent<Health>().health < this.GetComponent<Health>().MAXHEALTH
            && dialogPassed == false
        )
        {
            StartCoroutine(attackDia());
            dialogPassed = true;
        }
    }

    //A handler to start the dialogue in the event
    public void StartDialogue()
    {
        StartCoroutine(dialog());
    }

    private void movement()
    {
        StartCoroutine(move());
    }

    //A function that displays dialogue.
    //It runs through it sequentially
    private IEnumerator dialog()
    {
        yield return new WaitForSeconds(1);
        canvas.SetActive(true);

        yield return new WaitForSeconds(3);
        for (int i = 1; i < dialogue.Length; i++)
        {
            dialogue[i - 1].SetActive(false);
            dialogue[i].SetActive(true);
            yield return new WaitForSeconds(3);

            if (i == 2)
            {
                doorBox.enabled = true;
                canvas.SetActive(false);
            }
        }
    }

    //A function to show the attack dialogue
    private IEnumerator attackDia()
    {
        canvas.SetActive(true);
        foreach (GameObject child in dialogue)
        {
            child.SetActive(false);
        }
        aDialog.SetActive(true);
        yield return new WaitForSeconds(3);
        canvas.SetActive(false);
    }

    //A function to move
    private IEnumerator move()
    {
    float time = 2;
    while (time != 10) {
        print("megaloop");

        time = 3;
        while (time > 0)
        {
            time -= Time.deltaTime;

            this.GetComponent<Animator>().SetFloat("h", -1);
            this.transform.position = Vector3.MoveTowards(
                this.transform.position,
                spawnPoints[0].transform.position,
                (1.0f * Time.deltaTime)
            );
        }

        yield return new WaitForSeconds(2);

        time = 3;

        while (time > 0)
        {
            time -= Time.deltaTime;
            this.GetComponent<Animator>().SetFloat("h", 1);
            this.transform.position = Vector2.MoveTowards(
                this.transform.position,
                spawnPoints[1].transform.position,
                (1.0f * Time.deltaTime)
            );
        }
        yield return new WaitForSeconds(2);
        yield return null;
    }
        
    }

    // Gets the collisions
    private IEnumerator OnTriggerEnter2D(Collider2D collide)
    {
        // If tag is enemy and it has health
        if (collide.CompareTag("Player") && collide.GetComponent<Health>() != null)
        {
            collide.gameObject.SetActive(true);
            // Initialises instance
            Health healthVar = collide.GetComponent<Health>();

            // Decrements health
            healthVar.healthDecrement(5);

            yield return new WaitForSeconds(5);
        }
    }
}
