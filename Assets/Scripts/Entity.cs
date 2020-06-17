using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
