using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltTowardCameraScript : MonoBehaviour
{
    public bool x, y;

    Camera cam;

    void Start() {
        cam = Camera.main;
    }

    void Update() {
        float thetaX = x ? cam.transform.localRotation.eulerAngles.x : 0;
        float thetaY = y ? cam.transform.localRotation.eulerAngles.y : 0;
        transform.localRotation = Quaternion.Euler(thetaX, thetaY, 0);
    }
}
