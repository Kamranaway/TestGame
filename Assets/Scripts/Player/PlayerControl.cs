
using System.Dynamic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using InputType = InputProcess.InputType;

/*
 * 
 * Script PlayerControl is responsible for updating input states
 * 
 * Please tune Serialized Fields in unity editor as opposed to in file
 */
public class PlayerControl: MonoBehaviour
{
    public Animator animator;

    //See Input Process for description of input types

    //General controls
    public InputProcess toggleCursor = new InputProcess("Cursor Toggle" , InputType.TOGGLE, KeyCode.LeftControl);
    public InputProcess menu = new InputProcess("Menu", InputType.TOGGLE, KeyCode.Escape);

    //Combat controls
    public InputProcess constantFireR = new InputProcess("Constant FireR", InputType.CONSTANT, (int) MouseButton.RightMouse);
    public InputProcess constantFireL = new InputProcess("Constant FireL", InputType.CONSTANT, (int) MouseButton.LeftMouse);
    public InputProcess instantFireR = new InputProcess("Instant FireR", InputType.INSTANT, (int) MouseButton.RightMouse);
    public InputProcess instantFireL = new InputProcess("Instant FireL", InputType.INSTANT, (int) MouseButton.LeftMouse);
    public InputProcess shiftLeftSpell = new InputProcess("Shift Left Spell", InputType.INSTANT, KeyCode.Q);
    public InputProcess shiftRightSpell = new InputProcess("Shift Right Spell", InputType.INSTANT, KeyCode.E);

    public bool firing = false;

    private void Awake()
    {
        animator = FindObjectOfType<PlayerMovement>().GetComponent<Animator>();
    }

    void Start()
    { }

    void Update()
    {
        GeneralControl();
        AnimationUpdate();
        ProcessInputs();
    }


    void LateUpdate()
    {
       
    }

    void GeneralControl()
    {
        if ( menu.inputDown )
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /*
     * Control processes must be updated to scan for input events
     */
    void ProcessInputs()
    {
        menu.ProcessLoop();
        constantFireR.ProcessLoop();
        constantFireL.ProcessLoop();
        instantFireL.ProcessLoop();
        instantFireR.ProcessLoop();
        toggleCursor.ProcessLoop();
        shiftLeftSpell.ProcessLoop();
        shiftRightSpell.ProcessLoop();
    }

    void AnimationUpdate()
    {
        if ( constantFireR.inputDown == true || constantFireL.inputDown == true ) { firing = true; } else { firing = false; }
        animator.SetBool("firing", firing);
    }

}
