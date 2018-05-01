using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject controllerEnities;

    public Transform controllerTransform;   // Use a copy of the controller camera transform (not as child of controller entities!)

    public float rotationSpeed;
    public float translationSpeed;

    private bool moveCamera = false;
    private bool activateController = false;

    private void Awake()
    {
        SetActiveAllChildren(controllerEnities.transform, false);   // Disable everything that belongs to the controller
        mainCamera.enabled = true;                                  // Make sure that the main camera is on
    }

    private void Update()
    {
        // Called when button is clicked
        if (moveCamera)
        {
            // Rotate camera to the controller rotation
            mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, 
                                                                    controllerTransform.rotation, 
                                                                    Time.deltaTime * rotationSpeed * 10);
            // Move camera to the controller position
            mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position,
                                                                controllerTransform.position,
                                                                Time.deltaTime * translationSpeed * 10);

            // Activate controller when camera has moved to the right position and has finished rotation
            if (mainCamera.transform.position == controllerTransform.position && mainCamera.transform.rotation == controllerTransform.rotation)
            {
                moveCamera = false;
                activateController = true;
            }
        }
        else if (activateController)
        {
            SetActiveAllChildren(controllerEnities.transform, true);    // Enable everything that belongs to the controller
            mainCamera.enabled = false;                                 // Disable main camera
        }
    }

    // Call this method on button to activate first person controller
    public void ActivateControllerCamera()
    {
        moveCamera = true;
    }

    // Check for every child and disable or enable it
    private void SetActiveAllChildren(Transform transform, bool value)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(value);
            SetActiveAllChildren(child, value);
        }
    }
}
