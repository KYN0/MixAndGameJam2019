using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class aim : MonoBehaviour
{
    [Header("-properties-")]
    public CinemachineFreeLook thirdPersonCam;
    private CinemachineImpulseSource impulse;
    CinemachineComposer comp;

    [Header("Settings")]
    private float originalZoom;
    public float originalOffsetAmount;
    public float zoomOffsetAmount;
    public float translateAimX,translateAimY,translateAimZ;
    public bool isAiming;
    
    private Quaternion playerRotation;
    public float MIN_VALUE, MAX_VALUE, smooth, noise,zoomIn, zoomOut;
    private Vector3 fixedRotation;
    // Start is called before the first frame update
    void Start()
    {
        zoomOut = thirdPersonCam.m_Lens.FieldOfView;
        originalZoom = thirdPersonCam.m_Orbits[1].m_Radius;
        impulse = thirdPersonCam.GetComponent<CinemachineImpulseSource>();
        fixedRotation.x = transform.rotation.x;
        comp = thirdPersonCam.GetRig(1).GetCinemachineComponent<CinemachineComposer>();
        Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
          if (Input.GetButtonDown("Fire2"))
        {
            isAiming = true;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isAiming = false;
        }

        
    }

    void LateUpdate()
    {

        
        if (isAiming.Equals(true))
        {
            //laserSight.enabled= true;
            thirdPersonCam.m_Lens.FieldOfView = zoomIn;
            comp.m_TrackedObjectOffset.x = translateAimX;

        }
        else if(isAiming.Equals(false))
        {
            //  laserSight.enabled = false;
            thirdPersonCam.m_Lens.FieldOfView = zoomOut;
        }
    }
}
