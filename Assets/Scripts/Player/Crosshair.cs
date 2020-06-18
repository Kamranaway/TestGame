
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Cursor = UnityEngine.Cursor;

/*
 * Script responsible for crosshair control and change.
 */
public class Crosshair : MonoBehaviour
{
    [SerializeField] float maxRadius = 1;
    GameObject longCursor;
    GameObject shortCursor;
    GameObject currentCursor;
    GameObject centerPivot;
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
       
        currentCursor.transform.position = newPos;
        lastCharPos = centerPivot.transform.position;
    }

    /*
     * Obtains position of the cursor
     */
    public float[] GetCursorPos()
    {
        float x = currentCursor.transform.position.x;
        float y = currentCursor.transform.position.y;
        float[] pos = { x, y };
        return pos;
    }

    /*
     * Updates current cursor position
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

        if ( shortHand && !longCursorSprite.forceRenderingOff )
        {
            longCursor.transform.position = shortCursor.transform.position;
            longCursorSprite.forceRenderingOff = true;
            shortCursorSprite.forceRenderingOff = false;

        }
        else if ( !shortHand && longCursorSprite.forceRenderingOff )
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
