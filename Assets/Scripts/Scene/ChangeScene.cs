using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    // instance of animator
    public Animator animator;

    // A function to close the game entirely
    public void shut_down_game()
    {
        Application.Quit();
    }

    // a function to load the scenes.
    // @param scene - name of the scene to be loaded as a string
    IEnumerator loadSceneTransitions(string scene)
    {
        // Calls FadeOut which triggers fade out
        animator.SetTrigger("FadeOut");
        // Waits a second
        yield return new WaitForSeconds(1f);
        // Loads scene
        SceneManager.LoadScene(scene);

        // fades_in
        animator.SetTrigger("FadeIn");
    }

    // Loads scene
    //@param scenetoload - name of the scene
    public void loadScene(string sceneToLoad)
    {
        // Starts a Coroutine (basically multithreading)    
        StartCoroutine(loadSceneTransitions(sceneToLoad));
    }

}
