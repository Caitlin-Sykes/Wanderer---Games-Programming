using UnityEngine;

public class Connie : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        anim = this.gameObject.GetComponent<Animator>();
        setAnimator(0,0,false);

    }

    //A function to set the animator for Connie
    //@param: v as vertical transform
    //@param: h as horizontal transform
    //@param: move as moving boolean
    private void setAnimator(int v, int h, bool move)
    {
        anim.SetBool("is_moving", move);
        anim.SetFloat("vertical", v);
        anim.SetFloat("horizontal", h);

    }

    //A function to trigger wakeup
    public void wakeUp() {
        anim.SetTrigger("wake");
    }

  

}
