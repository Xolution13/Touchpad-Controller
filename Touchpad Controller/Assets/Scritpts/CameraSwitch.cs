using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject mainCameraObject;
    private Camera mainCamera;
    public GameObject transitionCameraPrefab;
    private Camera transitionCamera;
    public GameObject controllerEnities;
    private GameObject newCamera;

    public Transform controllerTransform;   // Use a copy of the controller camera transform (not as child of controller entities!)

    public float rotationSpeed;
    public float translationSpeed;

    private bool moveCamera = false;
    private bool activateController = false;

    private void Awake()
    {
        mainCamera = mainCameraObject.GetComponent<Camera>();
        SetActiveAllChildren(controllerEnities.transform, false);   // Disable everything that belongs to the controller
        mainCamera.enabled = true;                                  // Make sure that the main camera is on
    }

    private void Update()
    {
        // Called when button is clicked
        if (moveCamera)
        {
            // Rotate camera to the controller rotation
            transitionCamera.transform.rotation = Quaternion.RotateTowards(transitionCamera.transform.rotation, 
                                                                    controllerTransform.rotation, 
                                                                    Time.deltaTime * rotationSpeed * 10);
            // Move camera to the controller position
            transitionCamera.transform.position = Vector3.MoveTowards(transitionCamera.transform.position,
                                                                controllerTransform.position,
                                                                Time.deltaTime * translationSpeed * 10);

            // Activate controller when camera has moved to the right position and has finished rotation
            if (transitionCamera.transform.position == controllerTransform.position && transitionCamera.transform.rotation == controllerTransform.rotation)
            {
                moveCamera = false;
                activateController = true;
            }
        }
        else if (activateController)
        {
            SetActiveAllChildren(controllerEnities.transform, true);    // Enable everything that belongs to the controller
            transitionCamera.enabled = false;                           // Disable main camera
        }
    }

    // Call this method on button to activate first person controller
    public void ActivateControllerCamera()
    {
        // Get main camera rotation
        Quaternion mainCameraRotation = mainCameraObject.transform.rotation;

        // Instantiate new Camera prefab
        newCamera = Instantiate(transitionCameraPrefab, mainCameraObject.transform.position, mainCameraRotation);

        // Get new camera object of that prefab
        transitionCamera = newCamera.GetComponent<Camera>();

        // Disable main camera and enable new camera
        mainCamera.enabled = false;
        transitionCamera.enabled = true;
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
