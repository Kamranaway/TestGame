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
    public InputProcess constantFireR;
    public InputProcess constantFireL;
    public InputProcess instantFireR;
    public InputProcess instantFireL;
    
    

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        animator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
       
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
        
        SpellSwitch();



      
        AnimationUpdate();
        Debug.Log(instantFireR.inputDown);
    }

    void SpellSwitch() 
    {
       
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
