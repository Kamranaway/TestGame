using Cinemachine;
using Unity.Mathematics;
using UnityEngine;

/*
 * Class responsible for handling orbital camera control.
 */
public class OrbitalCam : MonoBehaviour
{
    [SerializeField] private float sensitivity = .25f;
    [SerializeField] private float maxOrbit = 2;
    private CinemachineOrbitalTransposer playerCamera;
    private float y;
    private float x;
    public bool orbitEnabled = false;


   private void Start()
    {
        playerCamera = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
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

       /* Vector2 centerPos = new Vector2(centerPivot.transform.position.x, centerPivot.transform.position.y);
        float radius = Vector2.Distance(newPos, centerPos);
        float maxRadius = 1;
        if ( radius > maxRadius )
        {
            Vector2 diff = newPos - centerPos;
            diff *= maxRadius / radius;

            newPos = centerPos + diff;
        }*/


        playerCamera.m_FollowOffset.x = -x;
        playerCamera.m_FollowOffset.y = -y;
    }
}
