using UnityEngine;

public class TiltGlobalTowardCameraScript : MonoBehaviour {
    public bool x, y;

    Camera cam;

    void Start() {
        cam = Camera.main;
    }

    void Update() {
        float thetaX = x ? cam.transform.rotation.eulerAngles.x : 0;
        float thetaY = y ? cam.transform.rotation.eulerAngles.y : 0;
        transform.rotation = Quaternion.Euler(thetaX, thetaY, 0);
    }
}
