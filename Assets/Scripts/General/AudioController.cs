using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{

    public static AudioSource currentAudio { get; set; }//the song currently playing
    public GameObject audioGO; // the parent gameobject of all audio

    
    void Awake() {
        changeSong("Main");
    }

    // A function to change the song
    public void changeSong(string newSong)
    {

        if (currentAudio != null)
        {
            StartCoroutine(StartFade(currentAudio, 0));
        };

        if (getAudio(newSong))
        {
            currentAudio.volume = 0;
            currentAudio.Play();
            StartCoroutine(StartFade(currentAudio, 1));

        };
    }






    // Gets the audio object
    private bool getAudio(string song)
    {

        try
        {
            currentAudio = GameObject.Find("/EventSystem/Audio/" + song).GetComponent<AudioSource>(); //sets currentAudio to the new song
            return true;
        }


        catch
        {
            print("Couldn't find the audio file. Have they been altered in some way?");
            return false;
        }

    }




    // The following function is adapted from https://johnleonardfrench.com/how-to-fade-audio-in-unity-i-tested-every-method-this-ones-the-best/
    // It is method 1 on the page
    private IEnumerator StartFade(AudioSource audioSource, float targetVolume)
    {
        float currentTime = 0;
        float duration = 5;

        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }


}
