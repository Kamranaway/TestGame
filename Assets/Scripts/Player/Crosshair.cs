
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Cursor = UnityEngine.Cursor;

public class Crosshair : MonoBehaviour
{


    [SerializeField] float maxRadius = 1;
    public float angleToPlayer = 0;
    PlayerControl playerControl;
    float lastX = 0;
    float lastY = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        playerControl = FindObjectOfType<PlayerControl>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Start()
    {
     
    }

    
    // Update is called once per frame
    void Update()
    {
        TranslateCrosshair();
        if ( (playerControl.mouse1.inputDown || playerControl.mouse2.inputDown)  ) 
        {
            
            playerControl.playerFaceAngle = angleToPlayer;
            
        }
    }

    private void TranslateCrosshair() {

   

        float[] pos = FindObjectOfType<PlayerControl>().GetPlayerPos();
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float XPrime = mouseX + transform.position.x;
        float YPrime = mouseY + transform.position.y;
        Vector2 newPos = new Vector2(XPrime, YPrime);
        Vector2 centerPos = new Vector2 (pos[0], pos[1]);
        float radius = Vector2.Distance(newPos, centerPos);
        angleToPlayer = Mathf.Atan2(newPos.y - centerPos.y, newPos.x - centerPos.x);
        angleToPlayer = angleToPlayer * Mathf.Rad2Deg;
        angleToPlayer = (angleToPlayer < 0) ? angleToPlayer + 360 : angleToPlayer;
 

        if ( radius > maxRadius ) 
        {
            Vector2 diff = newPos - centerPos; 
            diff *= maxRadius / radius; 
            newPos = centerPos + diff; 
        }

        transform.position = newPos ;
       

     
    }

  
}
