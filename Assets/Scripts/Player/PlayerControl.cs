
using System.Dynamic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;

/*
 * 
 * Script PlayerControl is responsible for updating input states
 * 
 * Please tune Serialized Fields in unity editor as opposed to in file
 */
public class PlayerControl: MonoBehaviour
{
 
    public InputProcess toggleCursor = new InputProcess("Cursor Toggle" ,true, KeyCode.LeftControl);
    public InputProcess menu = new InputProcess("Menu", true, KeyCode.Escape);
    public InputProcess fireR = new InputProcess("FireR", false, 0);
    public InputProcess fireL = new InputProcess("FireL", false, 1);
  

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
        if ( menu.inputToggle )
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
        fireR.ProcessLoop();
        fireL.ProcessLoop();
        toggleCursor.ProcessLoop();
        
    }

}
