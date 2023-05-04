using UnityEngine;
using UnityEngine.Events;

public class OnScene : MonoBehaviour
{
    public UnityEvent onGameOverEvent;

    public void gameOver() {
        onGameOverEvent.Invoke();
    }
}
