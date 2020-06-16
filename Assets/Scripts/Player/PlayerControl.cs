
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
public class PlayerControl: Entity
{
    [Range(0, 1)] [SerializeField] float speedFactor = 1; //Tune in context menu for desired player speed
    Crosshair crosshair;
    Animator animator;
    public Vector2 input;
    Rigidbody2D rb2d;
    float max_speed = 1000;
    float speed = 0;
    public float playerFaceAngle = 0;
    public InputProcess leftCtrl = new InputProcess(true, KeyCode.LeftControl);
    public InputProcess mouse1 = new InputProcess(false, 0);
    public InputProcess mouse2 = new InputProcess(false, 1);

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


    /*
     * Update is called once per frame
     * 
     * Currently Updates inputs and player movements; based on CPU
     */
    void Update()
    {
        InputProcesses();
        MovePlayer();
        Attack();
    }

    /*
     * From UNITY Docs:
     * "MonoBehaviour.FixedUpdate has the frequency of the physics system; it is called every fixed frame-rate frame. Compute Physics system calculations after FixedUpdate. 0.02 seconds (50 calls per second) is the default time between calls. Use Time.fixedDeltaTime to access this value. Alter it by setting it to your preferred value within a script, or, navigate to Edit > Settings > Time > Fixed Timestep and set it there. The FixedUpdate frequency is more or less than Update. If the application runs at 25 frames per second (fps), Unity calls it approximately twice per frame, Alternatively, 100 fps causes approximately two rendering frames with one FixedUpdate. Control the required frame rate and Fixed Timestep rate from Time settings. Use Application.targetFrameRate to set the frame rate."
     */
    private void FixedUpdate()
    {   
    }

    /*
     * Method responsible for player positioning based on inputs
     */
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
        Debug.Log(speed);

        rb2d.velocity = input * speedFactor * Time.deltaTime * max_speed; // Multiplied by time to be functionally independent of frame rate
    }

    /*
     * Skeleton for attack system; advise using another class 
     */
    void Attack()
    {
        if ( mouse1.inputDown )
        {
            animator.SetBool("fireL", true);

        }
        else
        {
            animator.SetBool("fireL", false);
        }

        if ( mouse2.inputDown )
        {
            animator.SetBool("fireR", true);
        }
        else
        {
            animator.SetBool("fireR", false);
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
        Debug.Log(leftCtrl.inputToggle);
    }

    /*
     * 
     * Returns the player world position as an array of [x,y]
     * 
     */
    public float[] GetPlayerPos()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        float[] pos = { x, y };
        return pos;
    }

}
