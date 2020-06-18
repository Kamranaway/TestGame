using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Script responsible for handling sprite sorting order; attach to objects who may pass behind/in front of other objects.
 */
[ExecuteAlways]
public class SpriteSort : MonoBehaviour
{
    [SerializeField] GameObject pivot;
    Renderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }
   
    void LateUpdate()
    {
        spriteRenderer.sortingOrder = (int) (transform.position.y * -10);
    }
}
