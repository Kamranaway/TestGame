using System;
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
    public bool instantFireL = false;
    public bool constantFireL = false;
    public bool instantFireR = false;
    public bool constantFireR = false;
    

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
    
    }

    // Update is called once per frame
    void Update()
    {
      //  leftSpell.GetComponent<Spell>().


        constantFireR = playerControl.fireR.inputDown;
        instantFireR = playerControl.fireR.instantInputDown;
        constantFireL = playerControl.fireL.inputDown;
        instantFireL = playerControl.fireL.instantInputDown;
      
        AnimationUpdate();
    }

    void AnimationUpdate() 
    {


        animator.SetBool("fireR", constantFireR);

        animator.SetBool("fireL", constantFireL);
   
    }
    private void LateUpdate()
    {
        
    }
}
