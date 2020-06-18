using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerCharacter : Entity
{
   [Range(0f, 100f)] [SerializeField] public float mana = 100;
    [Range(0f, 100f)] [SerializeField] public float health = 100;
    public PlayerCharacter() 
    { 
    
    }

    public void Update()
    {
        Debug.Log("check");
    }
}
