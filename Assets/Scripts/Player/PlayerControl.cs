
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
 
    public InputProcess toggleCursor = new InputProcess("Cursor Toggle" , InputType.TOGGLE, KeyCode.LeftControl);
    public InputProcess menu = new InputProcess("Menu", InputType.TOGGLE, KeyCode.Escape);
    public InputProcess constantFireR = new InputProcess("FireR", InputType.CONSTANT, (int) MouseButton.RightMouse);
    public InputProcess constantFireL = new InputProcess("FireL", InputType.CONSTANT, (int) MouseButton.LeftMouse);
    public InputProcess instantFireR = new InputProcess("FireR", InputType.INSTANT, (int) MouseButton.RightMouse);
    public InputProcess instantFireL = new InputProcess("FireL", InputType.INSTANT, (int) MouseButton.LeftMouse);


    // Start is called before the first frame update
    void Start()
    { }

    void Update()
    {
        GeneralControl();
    }

   
    void LateUpdate()
    {
        InputProcesses();
       
    }

   

    void GeneralControl()
    {
        if ( menu.inputDown )
        {
            //PauseGame();
        }
        else
        {
            //ResumeGame();
        }
    }

    /*
     * Control processes from PlayerControl class
     */
    void InputProcesses()
    {
        constantFireR.ProcessLoop();
        constantFireL.ProcessLoop();
        instantFireL.ProcessLoop();
        instantFireR.ProcessLoop();
        toggleCursor.ProcessLoop();
        
    }

}
