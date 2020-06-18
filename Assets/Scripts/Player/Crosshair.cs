
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Cursor = UnityEngine.Cursor;
using Cinemachine;
using Vector3 = UnityEngine.Vector3;

/*
 * Script responsible for crosshair control and change.
 */
public class Crosshair : MonoBehaviour
{
    [SerializeField] float maxRadius = 1;
    [Range(0f,10f)][SerializeField] float mouseBorderOffsetX = 0;
    [Range(0f, 10f)][SerializeField] float mouseBorderOffsetY = 0;
    GameObject longCursor;
    GameObject shortCursor;
    GameObject currentCursor;
    GameObject centerPivot;
    Camera playerCamera;
    OrbitalCam orbitalCam;
    SpriteRenderer longCursorSprite;
    SpriteRenderer shortCursorSprite;
    public float angleToPlayer = 0;
    PlayerControl playerControl;
    bool shortHand = false;
    Vector2 lastCharPos = new Vector2(0,0);
 

    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        shortCursor = GameObject.Find("Cursor_Short"); 
        longCursor = GameObject.Find("Cursor_Long");
        centerPivot = GameObject.Find("Char_Center");
        orbitalCam = FindObjectOfType<OrbitalCam>();
        longCursorSprite = longCursor.GetComponent<SpriteRenderer>();
        shortCursorSprite = shortCursor.GetComponent<SpriteRenderer>();
        playerCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
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
        }

        shortCursorSprite.forceRenderingOff = true;
    }

    private void FixedUpdate()
    {
        
    }

    void Update()
    {
        UpdateCursorAngle();
        CheckCursorToggle();
    }

    private void LateUpdate()
    {
        
        TranslateCrosshair();
    }

    /*
     * Translates crosshair position based on mouse input.
     */
    private void TranslateCrosshair() {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float XPrime = mouseX + currentCursor.transform.position.x;
        float YPrime = mouseY + currentCursor.transform.position.y;
        Vector2 newPos = new Vector2(XPrime, YPrime) + ((Vector2) centerPivot.transform.position - lastCharPos);
        Vector2 centerPos = new Vector2 (centerPivot.transform.position.x, centerPivot.transform.position.y);
        float radius = Vector2.Distance(newPos, centerPos);
        angleToPlayer = Mathf.Atan2(newPos.y - centerPos.y, newPos.x - centerPos.x);
        angleToPlayer = angleToPlayer * Mathf.Rad2Deg;
        angleToPlayer = (angleToPlayer < 0) ? angleToPlayer + 360 : angleToPlayer;
 

        if ( radius > maxRadius && shortHand) 
        {
            Vector2 diff = newPos - centerPos; 
            diff *= maxRadius / radius;
           
            newPos = centerPos + diff; 
        }

        float Xmin = playerCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float Xmax = playerCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float Ymin = playerCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        float Ymax = playerCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        newPos.x = Mathf.Clamp(newPos.x, Xmin+mouseBorderOffsetX, Xmax-mouseBorderOffsetX); //temporary solution
        newPos.y = Mathf.Clamp(newPos.y, Ymin+mouseBorderOffsetY, Ymax-mouseBorderOffsetY); //temporary solution

        currentCursor.transform.position = newPos;
        lastCharPos = centerPivot.transform.position;
    }

    /*
     * Obtains position of the cursor
     */
    public Vector2 GetCursorPos()
    {
        float x = currentCursor.transform.position.x;
        float y = currentCursor.transform.position.y;
        return new Vector2(x,y);
    }

    /*
     * Updates current cursor angle relative to the player
     */
    private void UpdateCursorAngle()
    {
        if ( (playerControl.mouse1.inputDown || playerControl.mouse2.inputDown) )
        {
            playerControl.playerFaceAngle = angleToPlayer;
        }
    }

    /*
     * Checks and updates cursor toggle.
     */
    private void CheckCursorToggle() 
    {
        currentCursor = (playerControl.leftCtrl.inputToggle && currentCursor != shortCursor) ? shortCursor :
              (!playerControl.leftCtrl.inputToggle && currentCursor != longCursor) ? longCursor : currentCursor;

        shortHand = (playerControl.leftCtrl.inputToggle && !shortHand) ? true :
            (!playerControl.leftCtrl.inputToggle && shortHand) ? false : shortHand;

        if ( shortHand )
        {
            longCursor.transform.position = shortCursor.transform.position;
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
