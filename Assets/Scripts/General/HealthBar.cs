using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    public Slider slide; //health slider
    public GameObject chara; //Player Gameobject

    private Health ch; //instance of health
    

    void Awake()
    {
        Canvas canva = this.gameObject.GetComponent<Canvas>();
        canva.worldCamera = Camera.main;
        try {
            ch = chara.GetComponent<Health>();
            slide.maxValue = ch.MAXHEALTH;
            slide.value = ch.health;
        }

        catch { 
            // TODO: figure out why this throws
            print("beep");
        }
    }

    // Sets the health bar to the current health
    public void healthSet() {
        slide.value = ch.health;
    }
}
