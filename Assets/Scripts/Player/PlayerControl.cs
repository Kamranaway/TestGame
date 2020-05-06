
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;

/*
 * 
 * Script PlayerControl is responsible for updating input states and animation states for the player
 * 
 */
public class PlayerControl: MonoBehaviour
{
    // [SerializeField] public Animator animator;
    [Range(0, 1)] [SerializeField] float speedFactor = 1;
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


    private void Awake()
    {
        crosshair = FindObjectOfType<Crosshair>();
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        InputProcesses();
        MovePlayer();
        Attack();
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


        rb2d.velocity = input * speedFactor * Time.deltaTime * max_speed;
    }

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
