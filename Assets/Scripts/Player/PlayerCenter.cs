using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script that causes a gameobject to follow the player. 
 * 
 * Used for Char_Center
 */
public class PlayerCenter : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] float offsetX = 0;
    [SerializeField] float offsetY = 0;
    private void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPos = playerMovement.GetPlayerPos();
        float newX = newPos.x;
        float newY = newPos.y;
        Vector2 translation = new Vector2(newX + offsetX, newY + offsetY);
        transform.position = translation;

    }
}
