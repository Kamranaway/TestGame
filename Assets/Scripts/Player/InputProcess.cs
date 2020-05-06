using UnityEngine;
using UnityEngine.UIElements;

/*
 * 
 * InputProcess is to be constructed/instantiated for desired input types.
 * The given input can be instantiated as a toggle or standard input.
 * Any instance of InputProcess must be called in the Update method of a script.
 * 
 */
public class InputProcess 
{
    //persistently true if condition is met
    public bool inputUp = false;
    public bool inputDown = false;
    public bool inputToggle = false;
    //true for only the instant of a given trigger frame
    public bool instantInputUp = false; 
    public bool instantInputDown = false; 

    private bool toggleMode;
    private KeyCode keyCode;
    private bool isMouseButton;
    private int mouseButton;

    //class constructor
    public InputProcess(bool toggleMode, KeyCode keyCode) 
    {
        this.toggleMode = toggleMode;
        this.keyCode = keyCode;
        this.isMouseButton = false;
    }
    
    public InputProcess(bool toggleMode, int mouseButton)
    {
        this.toggleMode = toggleMode;
        this.mouseButton = mouseButton;
        this.isMouseButton = true;
    }


    //must be called in Update method 
    public void ProcessLoop() 
    {
        if ( toggleMode ) 
        { 
            TogglePress(); 
        } 
        else 
        { 
            Press(); 
        }
    }

    private void TogglePress() 
    {
        if ( !isMouseButton )
        {
            if ( Input.GetKeyDown(keyCode) && !inputToggle )
            {
                inputToggle = true;
                inputUp = false;
            }
            else if ( Input.GetKeyDown(keyCode) && inputToggle && !inputUp )
            {
                inputToggle = false;
            }
            inputUp = (Input.GetKeyUp(keyCode) && !inputUp && inputToggle) ? true : false;
        }
        else 
        {
            if ( Input.GetMouseButtonDown(mouseButton) && !inputToggle )
            {
                inputToggle = true;
                inputUp = false;
            }
            else if ( Input.GetMouseButtonDown(mouseButton) && inputToggle && !inputUp )
            {
                inputToggle = false;
            }
            inputUp = (Input.GetMouseButtonUp(mouseButton) && !inputUp && inputToggle) ? true : false;
        }
    }

    private void Press()
    {
        if ( !isMouseButton )
        {
            if ( Input.GetKeyDown(keyCode) )
            {
                this.inputDown = true;
                this.inputUp = false;
            }
            else if ( Input.GetKeyUp(keyCode) )
            {
                this.inputDown = false;
                this.inputUp = true;
            }
            else
            {
                this.inputUp = false;
            }
            
            
        }
        else
        {

            if ( Input.GetMouseButtonDown(mouseButton) )
            {
                this.inputDown = true;
                this.inputUp = false;
                this.instantInputDown = true;
            }
            else if ( Input.GetMouseButtonUp(mouseButton) )
            {
                this.inputDown = false;
                this.inputUp = true;
                this.instantInputUp = true;
            }
            else
            {
                this.instantInputUp = false;
                this.instantInputDown = false;
            }

        }
    }
}
