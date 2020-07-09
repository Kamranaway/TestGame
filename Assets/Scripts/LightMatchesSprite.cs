using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

[ExecuteInEditMode]
public class LightMatchesSprite : UnityEngine.MonoBehaviour
{
    Light2D light2D;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        light2D = GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        light2D.color = spriteRenderer.color;
    }
}
