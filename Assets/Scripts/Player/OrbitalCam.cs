using Cinemachine;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

/*
 * Class responsible for handling orbital camera control.
 */
public class OrbitalCam : PlayerScript
{
    [SerializeField] private float sensitivity = .25f;
    [SerializeField] private float maxOrbit = 2;
    private float maxRadius;
    GameObject centerPivot;
    private CinemachineOrbitalTransposer playerCamera;
    private float y;
    private float x;
    public bool orbitEnabled = false;

    private void Awake()
    {
        playerCamera = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
        centerPivot = GameObject.Find("Char_Center");
    }
    private void Start()
    {
        maxRadius = maxOrbit / 4;
        x = 0;
        y = 0;
    }
    private void Update()
    {
        if ( orbitEnabled )
        {
            Orbit();
        }
        else
        {
            playerCamera.m_FollowOffset.x = 0;
            playerCamera.m_FollowOffset.y = 0;
        }
    }

    private void Orbit() 
    {

        x -= +Input.GetAxis("Mouse X") * sensitivity;
        y -= +Input.GetAxis("Mouse Y") * sensitivity;
        x = Mathf.Clamp(x, -maxOrbit, maxOrbit);
        y = Mathf.Clamp(y, -maxOrbit, maxOrbit);
    

        Vector2 centerPos = new Vector2(0, 0);
        Vector2 newPos = new Vector2(x, y);
        float radius = Vector2.Distance(newPos, centerPos);
      
   

        if ( radius > maxRadius )
        {
            Vector2 diff = newPos - centerPos;
            diff *= maxRadius / radius;
            newPos = centerPos + diff;
        }


        playerCamera.m_FollowOffset.x = -newPos.x;
        playerCamera.m_FollowOffset.y = -newPos.y;
    }
}
