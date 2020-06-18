using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Abstract class that defines entities; enemy, player, animal, projectile, etc...
 * Typically an object in the game work whose position will vary.
 * 
 */
 public abstract class Entity: MonoBehaviour
{

    public Entity()
    {

    }

    public float[] GetEntityPos()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float[] pos = { x, y };
        return pos;
    }

}
