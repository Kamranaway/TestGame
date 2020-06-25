using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellControl : MonoBehaviour
{

    PlayerControl playerControl;
    Animator animator;
    [SerializeField] GameObject[] spells;
    [SerializeField] GameObject leftSpell;
    [SerializeField] GameObject rightSpell;

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        animator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
        leftSpell = spells[ 0 ];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControl.fireR.inputDown ) 
        {
            Debug.Log("input down");
        }

        // Debug.Log(playerControl.mouse1.inputUp);
        //  Debug.Log(playerControl.mouse1.inputToggle);
        // Debug.Log(playerControl.mouse1.instantInputUp);
        //Debug.Log(playerControl.mouse1.instantInputDown);



        if ( playerControl.fireR.inputDown )
        {
            animator.SetBool("fireL", true);

        }
        else
        {
            animator.SetBool("fireL", false);
        }

        if ( playerControl.fireL.inputDown )
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
