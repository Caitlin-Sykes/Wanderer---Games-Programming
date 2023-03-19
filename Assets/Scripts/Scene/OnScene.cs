using UnityEngine;
using UnityEngine.Events;

public class OnScene : MonoBehaviour
{
    public UnityEvent enemyAttackEvent;
    public UnityEvent onGameOverEvent;


    void Start() {
        enemyAttackEvent.Invoke();

    }

    public void gameOver() {
        onGameOverEvent.Invoke();
    }
}
