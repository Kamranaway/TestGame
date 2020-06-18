
using System.Dynamic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;

/*
 * 
 * Script PlayerControl is responsible for updating input states and animation states for the player
 * 
 * Please tune Serialized Fields in unity editor as opposed to in file
 */
public class PlayerControl: PlayerCharacter
{
    [Range(0, 1)] [SerializeField] float speedFactor = 1; //Tune in context menu for desired player speed
    Crosshair crosshair;
    Animator animator;
    public Vector2 input;
    Rigidbody2D rb2d;
    float max_speed = 20;
    float speed = 0;
    public float playerFaceAngle = 0;
    public InputProcess leftCtrl = new InputProcess(true, KeyCode.LeftControl);
    public InputProcess mouse1 = new InputProcess(false, 0);
    public InputProcess mouse2 = new InputProcess(false, 1);
    public InputProcess escape = new InputProcess(true, KeyCode.Escape);

    /*
     * Get and store components/objects of use 
     */
    private void Awake()
    {
        crosshair = FindObjectOfType<Crosshair>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    { }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        GeneralControl();
    }

   
    void LateUpdate()
    {
        InputProcesses();
       
    }

    private void MovePlayer()
    {

        if ( input != Vector2.zero )
        {
            playerFaceAngle = Mathf.Atan2(input.y, input.x);
            playerFaceAngle = Mathf.Rad2Deg * playerFaceAngle;
            playerFaceAngle = (playerFaceAngle < 0) ? playerFaceAngle + 360 : playerFaceAngle;

            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
        }
   
        animator.SetFloat("playerFaceAngle", playerFaceAngle);

        animator.SetFloat("speed", speed);
        animator.Update(Time.deltaTime);
        animator.SetFloat("crosshairAngle", crosshair.angleToPlayer);


        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        speed = Mathf.Clamp(input.magnitude, 0, 1);
        

        rb2d.velocity = input * speedFactor * max_speed; // Multiplied by time to be functionally independent of frame rate
    }

    void GeneralControl()
    {
        if ( escape.inputToggle )
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    /*
     * Control processes from PlayerControl class
     */
    void InputProcesses()
    {
        mouse1.ProcessLoop();
        mouse2.ProcessLoop();
        leftCtrl.ProcessLoop();
        
    }

    /*
     * 
     * Returns the player world position as an array of [x,y]
     * 
     */
     

}
