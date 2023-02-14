using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class change_scene : MonoBehaviour
{
    // A function to load the launch screen
    public void load_launch_screen() {
        SceneManager.LoadScene("Launch_Screen");
    }

    // A function to close the game entirely
    public void shut_down_game() {
        Application.Quit();
    }

    // a function to show the credits
    public void show_credits() {
        SceneManager.LoadScene("Credits");
    }
}
