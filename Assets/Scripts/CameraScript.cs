using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraScript : MonoBehaviour
{
    public Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (MenuController.cameraMove)
        {


            float xAxisValue = Input.GetAxis("Horizontal") / 5;
            float zAxisValue = Input.GetAxis("Vertical") / 5;
            if (Camera.main != null)
            {
                Camera.main.transform.Translate(new Vector3(xAxisValue, zAxisValue, 0.0f));
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                mainCamera.orthographicSize -= 0.1f;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                mainCamera.orthographicSize += 0.1f;
            }
            if (mainCamera.orthographicSize <= 1.2f)
            {
                mainCamera.orthographicSize = 1.2f;
            }
            if (mainCamera.orthographicSize >= 25f)
            {
                mainCamera.orthographicSize = 25f;
            }

            transform.position = new Vector3(
              Mathf.Clamp(transform.position.x, -30f, 30f),
              Mathf.Clamp(transform.position.y, -30f, 30f),
              Mathf.Clamp(transform.position.z, -30f, 30f)
            );
        }
    }
}
