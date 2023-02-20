using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{

    // instance of animator
    public Animator animator;

    // A function to close the game entirely
    private void shut_down_game() {
        Application.Quit();
    }

    // a function to load the scenes.
    // takes in the name of the scene to be loaded as a string
     IEnumerator loadSceneTransitions(string scene) {
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
    public void loadScene(string sceneToLoad) {
        // Starts a Coroutine (basically multithreading)
        StartCoroutine(loadSceneTransitions(sceneToLoad));
    }

}
