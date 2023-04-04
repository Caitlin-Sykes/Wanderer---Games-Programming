using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{

    public Slider slide; //health slider
    public GameObject player; //Player Gameobject

    private Health ph; //instance of health
    

    void Awake()
    {
        ph = player.GetComponent<Health>();
        slide.maxValue = ph.MAXHEALTH;
        slide.value = ph.health;
        
    }

    // Sets the health bar to the current health
    public void healthSet() {
        slide.value = ph.health;
    }
}
