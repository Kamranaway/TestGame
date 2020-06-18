using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that causes a gameobject to follow the player. 
 * 
 * Used for Char_Center
 */
public class PlayerFollow : MonoBehaviour
{
    PlayerControl playerControl;
    [SerializeField] float offsetX = 0;
    [SerializeField] float offsetY = 0;
    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = playerControl.GetEntityPos();
        float newX = newPos.x;
        float newY = newPos.y;
        Vector2 translation = new Vector2(newX + offsetX, newY + offsetY);
        transform.position = translation;

    }
}
