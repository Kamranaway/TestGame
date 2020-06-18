using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;


/*
 * ABSTRACT
 * DO NOT ADD THIS SCRIPT TO A GAME OBJECT
 * 
 * Inherited by entity related scripts; objects, characters, npcs, enemies, the player, projectiles, other moving objects.
 * 
 * Not inherited by UI elements.
 */
public abstract class Entity: MonoBehaviour
{ 
    
    public Entity()
    {
        Debug.Log("test");
    }

    public Vector2 GetEntityPos()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        return new Vector2(x,y);
    }

}
