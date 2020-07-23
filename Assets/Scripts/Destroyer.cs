using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//Simple script to destroy a game object. May change implementation later.
public class Destroyer : MonoBehaviour
{
    public float projectileLife = 0;
    public GameObject projectile;

    private void OnDestroy()
    {
        
    }

    private void Start()
    {
        GameObject.Destroy(projectile, projectileLife);
    }
}
