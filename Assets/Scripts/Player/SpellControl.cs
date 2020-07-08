using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellControl : SpellScript
{
    PlayerControl playerControl;
    Animator animator;
    [SerializeField] GameObject[] spells;
    [SerializeField] GameObject leftSpell;
    [SerializeField] GameObject rightSpell;
    InputProcess constantFireR;
    InputProcess constantFireL;
    InputProcess instantFireR;
    InputProcess instantFireL;
    

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        animator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
        //leftSpell = spells[ 0 ];
        //rightSpell = spells[ 1 ];
    }
    // Start is called before the first frame update
    void Start()
    {
        constantFireR = playerControl.constantFireR;
        constantFireL = playerControl.constantFireL;
        instantFireR = playerControl.instantFireR;
        instantFireL = playerControl.instantFireL;
    }

    // Update is called once per frame
    void Update()
    {
      //  leftSpell.GetComponent<Spell>().



      
        AnimationUpdate();
    }

    void AnimationUpdate() 
    {


        animator.SetBool("fireR", constantFireR.inputDown);

        animator.SetBool("fireL", constantFireL.inputDown);
   
    }
    private void LateUpdate()
    {
        
    }
}
