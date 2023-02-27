using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Enemy", order = 1)]
public class EnemyType : ScriptableObject
{
    // Enemy Health
    public int health; 
    // Enemy Damage
    public int damage;

    // Enemy Speed
    public int speed;

    // Enemy Type
    public string type;
    
    
}
