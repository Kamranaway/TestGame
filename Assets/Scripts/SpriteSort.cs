using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SpriteSort : MonoBehaviour
{
    [SerializeField] GameObject pivot;
    Renderer renderer;
    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>(); 
    }
    // Update is called once per frame
    void LateUpdate()
    {
        renderer.sortingOrder = (int) (transform.position.y * -10);
        Debug.Log(renderer.sortingOrder);
        
       
    }
}
