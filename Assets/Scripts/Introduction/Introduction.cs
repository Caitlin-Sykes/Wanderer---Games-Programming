using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Introduction : MonoBehaviour
{
    public GameObject connie;

    public ChangeScene cs;
    public UnityEvent startIntro;

    public GameObject player;

    public GameObject canvas;

    private float time = 2;

    private bool active;

    public GameObject[] dialog;

    public GameObject evilBear;
    public bool dialoguePassed { get; set; } = false;

    public GameObject[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        player.GetComponent<Animator>().SetBool("sleep", true);
    }

    private void Update()
    {
        time -= Time.deltaTime; //subtracts from time real life seconds
        //if time = 0 then invoke event.
        if (time <= 0 && active == false)
        {
            startIntro.Invoke();
            active = true;
        }

        if (Input.GetKey(KeyCode.C) && Input.GetKey(KeyCode.Alpha8)) {
            cs.loadScene("1");
        }
    }

    //A handler to start the dialogue in the event
    public void StartDialogue()
    {
        StartCoroutine(dialogue());
    }

    //A function that displays dialogue.
    //It runs through it sequentially
    private IEnumerator dialogue()
    {
        canvas.SetActive(true);

        yield return new WaitForSeconds(3);
        for (int i = 1; i < dialog.Length; i++)
        {
            dialog[i - 1].SetActive(false);
            dialog[i].SetActive(true);

            if (i - 1 == 0)
            {
                player.GetComponent<Animator>().SetBool("sleep", false);
            }

            //Insures the dialog doesn't skip ahead of the player
            while (i == 2)
            {
                if (
                    Input.GetKey(KeyCode.W)
                    || Input.GetKey(KeyCode.A)
                    || Input.GetKey(KeyCode.S)
                    || Input.GetKey(KeyCode.D)
                )
                {
                    break;
                }
                else
                {
                    yield return null;
                }
            }

            //Insures the dialog doesn't skip ahead of the player
            while (i == 6)
            {
                if (
                    Input.GetKey(KeyCode.UpArrow)
                    || Input.GetKey(KeyCode.LeftArrow)
                    || Input.GetKey(KeyCode.DownArrow)
                    || Input.GetKey(KeyCode.RightArrow)
                )
                {
                    break;
                }
                else
                {
                    yield return null;
                }
            }

            //Reveals the bear
            if (i == 8)
            {
                evilBear.gameObject.SetActive(true);
            }

            //Insures the dialog doesn't skip ahead of the player
            while (i == 9)
            {
                if (Input.GetKey(KeyCode.H))
                {
                    connie.GetComponent<Animator>().SetTrigger("hide");
                    break;
                }
                else
                {
                    yield return null;
                }
            }

            //Moves the bear
            if (i == 9)
            {
                time = 2;
                while (evilBear.transform.localPosition != spawnPoints[0].transform.localPosition)
                {
                    time -= Time.deltaTime;
                    evilBear.GetComponent<EnemyMovement>().animationMovement(player.transform.position, evilBear.transform.position);
                    evilBear.transform.position = Vector3.Lerp(
                        evilBear.transform.position,
                        spawnPoints[0].transform.position,
                        (1.0f * Time.deltaTime)
                    );
                    yield return new WaitForEndOfFrame();

                    if (time <= 0)
                    {
                        break;
                    }
                }
            }

            //"Kidnaps" Connie and moves to exit
            if (i == 10)
            {
                time = 2;
                Destroy(connie);
                while (evilBear.transform.position != spawnPoints[1].transform.position)
                {
                    time -= Time.deltaTime;
                    evilBear.transform.position = Vector3.Lerp(
                        evilBear.transform.position,
                        spawnPoints[1].transform.position,
                        (1.0f * Time.deltaTime)
                    );
                    yield return new WaitForEndOfFrame();

                    if (time <= 0)
                    {
                        Destroy(evilBear);
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(3);
        }

        //hides text box again
        canvas.SetActive(false);

        //allows player to leave
        dialoguePassed = true;
    }
}
