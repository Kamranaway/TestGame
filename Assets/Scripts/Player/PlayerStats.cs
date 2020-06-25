using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    [Range(0f, 100f)] [SerializeField] public float mana = 100;
    [Range(0f, 100f)] [SerializeField] public float health = 100;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

}
