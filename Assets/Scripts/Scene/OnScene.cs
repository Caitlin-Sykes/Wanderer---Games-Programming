using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnScene : MonoBehaviour
{
    public UnityEvent enemyAttackEvent;


    void Start() {
        enemyAttackEvent.Invoke();
    }
}
