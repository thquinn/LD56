using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    Vector3 lookAt;
    float distance = 6;
    public float rotateSensitivity;
    public float panSensitivity;
    public float scrollSensitivity;
    float horizontalAngle = -Mathf.PI / 2;
    float verticalAngle = Mathf.PI / 4;

    private void Update() {
        // Input.
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
            lookAt += (Input.GetKey(KeyCode.A) ? -1 : 1) * transform.right * panSensitivity * Time.deltaTime * distance;
        }
        Vector3 forwardMoveVector = transform.forward;
        forwardMoveVector.y = 0;
        forwardMoveVector.Normalize();
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W)) {
            lookAt += (Input.GetKey(KeyCode.S) ? -1 : 1) * forwardMoveVector * panSensitivity * Time.deltaTime * distance;
        }
        distance *= Mathf.Pow(scrollSensitivity, -Input.mouseScrollDelta.y);
        distance = Mathf.Clamp(distance, 3, 10);
        if (Input.GetMouseButton(1)) {
            horizontalAngle -= Input.GetAxis("Mouse X") * rotateSensitivity;
            verticalAngle -= Input.GetAxis("Mouse Y") * rotateSensitivity;
            verticalAngle = Mathf.Clamp(verticalAngle, Mathf.PI * .1f, Mathf.PI * .4f);
        }

        // Set position.
        float xzDistance = distance * Mathf.Cos(verticalAngle);
        float x = Mathf.Cos(horizontalAngle) * xzDistance;
        float y = Mathf.Sin(verticalAngle) * distance;
        float z = Mathf.Sin(horizontalAngle) * xzDistance;
        transform.localPosition = lookAt + new Vector3(x, y, z);
        transform.LookAt(lookAt);
    }
}
