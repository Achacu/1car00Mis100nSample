using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform camPos;
    [SerializeField] private float camFollowSmoothTime;

    private Vector3 cameraVelocity;
    private PlayerCam camRot;
    public void Awake()
    {
        camRot = transform.GetChild(0).GetComponent<PlayerCam>();
    }
    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.SmoothDamp(transform.position,
                                                   camPos.position,
                                                   ref cameraVelocity, camFollowSmoothTime);
        camRot.RotLateUpdate();
        
        //camPos.position;
    }
}
