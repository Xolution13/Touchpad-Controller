using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject firstPersonController;
    private Camera controllerCamera;

    private void Start()
    {
        controllerCamera = firstPersonController.GetComponentInChildren<Camera>();
    }

    // Main camera is enabled on awake
    private void Awake()
    {
        firstPersonController.SetActive(false);
        mainCamera.enabled = true;
    }

    // Call this method to activate first preson controller
    public void ActivateControllerCamera()
    {
        firstPersonController.SetActive(true);
        mainCamera.enabled = false;
        controllerCamera.enabled = true;
    }

}
