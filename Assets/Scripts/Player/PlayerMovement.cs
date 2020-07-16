using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] float speedFactor = 1; //Tune in context menu for desired player speed
    Crosshair crosshair;
    Animator animator;
    public Vector2 input;
    Rigidbody2D rb2d;
    float max_speed = 20;
    float speed = 0;
    public float playerFaceAngle = 0;

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
    {
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
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

    public Vector2 GetPlayerPos()
    {
        return transform.position;
    }
}
