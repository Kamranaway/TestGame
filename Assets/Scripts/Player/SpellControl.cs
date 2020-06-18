using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellControl : PlayerCharacter
{

    PlayerControl playerControl;
    Animator animator;

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        animator = FindObjectOfType<PlayerControl>().GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl.mouse1.instantInputDown ) 
        {
            Debug.Log("input down");
        }

        // Debug.Log(playerControl.mouse1.inputUp);
        //  Debug.Log(playerControl.mouse1.inputToggle);
        // Debug.Log(playerControl.mouse1.instantInputUp);
        //Debug.Log(playerControl.mouse1.instantInputDown);

        if ( playerControl.mouse1.inputDown )
        {
            animator.SetBool("fireL", true);

        }
        else
        {
            animator.SetBool("fireL", false);
        }

        if ( playerControl.mouse2.inputDown )
        {
            animator.SetBool("fireR", true);
        }
        else
        {
            animator.SetBool("fireR", false);
        }
    }

    private void LateUpdate()
    {
        
    }
}
