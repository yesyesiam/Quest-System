using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The camera to have control over")]
    public Camera controledCamera;
    [Tooltip("The speed at which the camera rotates to look up and down (calculated in degrees)")]
    public float rotationSpeed = 60f;
    [Tooltip("Whether or not to invert the look direction")]
    public bool invert = true;


    void Start()
    {
        SetUpCamera();
    }

    int waitForFrames = 3;
    int framesWaited = 0;

    void Update()
    {
        if (framesWaited <= waitForFrames)
        {
            framesWaited += 1;
            return;
        }
        ProcessRotation();
    }

    void SetUpCamera()
    {
        if (controledCamera == null)
        {
            controledCamera = GetComponent<Camera>();
        }
    }

    void ProcessRotation()
    {
        float verticalLookInput = Input.GetAxis("Mouse Y");
        Vector3 cameraRotation = controledCamera.transform.rotation.eulerAngles;
        float newXRotation = 0;
        if (invert)
        {
            newXRotation  = cameraRotation.x - verticalLookInput * rotationSpeed * Time.deltaTime;
        }
        else
        {
            newXRotation = cameraRotation.x + verticalLookInput * rotationSpeed * Time.deltaTime;
        }

        // clamp the rotation 360 - 270 is up 0 - 90 is down
        // Because of the way eular angles work with Unity's rotations we have to act differently when clamping the rotation
        if (newXRotation < 270 && newXRotation >= 180)
        {
            newXRotation = 270;
        }
        else if (newXRotation > 90 && newXRotation < 180)
        {
            newXRotation = 90;
        }
        controledCamera.transform.rotation = Quaternion.Euler(new Vector3(newXRotation, cameraRotation.y, cameraRotation.z));
    }
}
