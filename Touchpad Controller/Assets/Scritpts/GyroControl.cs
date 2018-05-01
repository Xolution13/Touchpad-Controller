using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;
    private Transform controller;
	
    private void Start()
    {
        controller = FindObjectOfType<CharacterController>().GetComponent<Transform>();

        //cameraContainer = new GameObject("Camera Container");
        //cameraContainer.transform.position = transform.position;
        transform.SetParent(controller);
        //cameraContainer.transform.SetParent(controller);

        gyroEnabled = EnableGyro();
    }

    private void Update()
    {
        if (gyroEnabled)
        {
            controller.Rotate(0, -gyro.rotationRateUnbiased.y, 0);
            //transform.Rotate(-gyro.rotationRateUnbiased.x, 0, 0);
        }
    }

    private bool EnableGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;

            controller.rotation = Quaternion.Euler(90f, 90f, 90f);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }

        return false;
    }
}
