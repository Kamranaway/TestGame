
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Cursor = UnityEngine.Cursor;
using Cinemachine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.Experimental.Rendering;

/*
 * Script responsible for crosshair control and change.
 * 
 * This code is a mess, I will fix it later :p
 */
public class Crosshair : PlayerScript
{
    [SerializeField] float maxRadius = 1;
    GameObject longCursor;
    GameObject shortCursor;
    GameObject currentCursor;
    GameObject centerPivot;
    [SerializeField] Texture2D cursor;
    Camera playerCamera;
    OrbitalCam orbitalCam;
    SpriteRenderer longCursorSprite;
    SpriteRenderer shortCursorSprite;
    public float angleToPlayer = 0;
    PlayerControl playerControl;
    PlayerMovement playerMovement;
    bool shortHand = false;
    Vector2 lastCharPos = new Vector2(0, 0);
    Vector2 lastCursorPos = new Vector2(0, 0);
 

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        

        shortCursor = GameObject.Find("Cursor_Short"); 
        longCursor = GameObject.Find("Cursor_Long");
        centerPivot = GameObject.Find("Char_Center");
        orbitalCam = FindObjectOfType<OrbitalCam>();
        longCursorSprite = longCursor.GetComponent<SpriteRenderer>();
        shortCursorSprite = shortCursor.GetComponent<SpriteRenderer>();
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        lastCharPos = centerPivot.transform.position;
        lastCursorPos = GetCursorPos();
    }

    void Start()
    {
        if ( shortHand )
        {
            currentCursor = shortCursor;
        }
        else
        {
            currentCursor = longCursor;
            shortCursorSprite.forceRenderingOff = true;
        }

        
    }

    private void FixedUpdate()
    {
       
    }

    void Update()
    {
        UpdateCursorAngle();
        CheckCursorToggle();
        TranslateCrosshair();
    }

    private void LateUpdate()
    {
       
    }

    /*
     * Translates crosshair position based on mouse input.
     */
    private void TranslateCrosshair()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float XPrime = mouseX + currentCursor.transform.position.x;
        float YPrime = mouseY + currentCursor.transform.position.y;
        Vector2 newPos = (shortHand) ? new Vector2(XPrime, YPrime) + ((Vector2) centerPivot.transform.position - lastCharPos) : new Vector2(XPrime, YPrime);
        Vector2 centerPos = new Vector2(centerPivot.transform.position.x, centerPivot.transform.position.y);
        float radius = Vector2.Distance(newPos, centerPos);
        angleToPlayer = Mathf.Atan2(newPos.y - centerPos.y, newPos.x - centerPos.x);
        angleToPlayer = angleToPlayer * Mathf.Rad2Deg;
        angleToPlayer = (angleToPlayer < 0) ? angleToPlayer + 360 : angleToPlayer;

        if ( shortHand ) 
        {
            Cursor.lockState = CursorLockMode.Locked;

            if ( radius > maxRadius)
            {
            Vector2 diff = newPos - centerPos;
            diff *= maxRadius / radius;
            newPos = centerPos + diff;
            }
            currentCursor.transform.position = newPos;
            lastCharPos = centerPivot.transform.position;
            
    }
    else
    {
            Cursor.lockState = CursorLockMode.Confined;
            Vector3 temp = Input.mousePosition;
            currentCursor.transform.position =Camera.main.ScreenToWorldPoint(temp);
        }

    }

    /*
     * Obtains position of the cursor
     */
    public Vector2 GetCursorPos()
    {
        if ( shortHand )
        {
            float x = currentCursor.transform.position.x;
            float y = currentCursor.transform.position.y;
            return new Vector2(x, y);
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f;
            return Camera.main.ScreenToWorldPoint(mousePos);
        }
    }

    /*
     * Updates current cursor angle relative to the player
     */
    private void UpdateCursorAngle()
    {
        if ( (playerControl.constantFireR.inputDown || playerControl.constantFireL.inputDown) )
        {
            playerMovement.playerFaceAngle = angleToPlayer;
        }
    }

    /*
     * Checks and updates cursor toggle.
     */
    private void CheckCursorToggle() 
    {
        currentCursor = (playerControl.toggleCursor.inputDown && currentCursor != shortCursor) ? shortCursor :
              (!playerControl.toggleCursor.inputDown && currentCursor != longCursor) ? longCursor : currentCursor;

        shortHand = (playerControl.toggleCursor.inputDown && !shortHand) ? true :
            (!playerControl.toggleCursor.inputDown && shortHand) ? false : shortHand;

        if ( shortHand )
        {
            longCursorSprite.forceRenderingOff = true;
            shortCursorSprite.forceRenderingOff = false;

        }
        else
        {
            shortCursor.transform.position = longCursor.transform.position;
            longCursorSprite.forceRenderingOff = false;
            shortCursorSprite.forceRenderingOff = true;
        }

        if ( orbitalCam.orbitEnabled != shortHand )
        {
            orbitalCam.orbitEnabled = shortHand;
        }
    }


}
