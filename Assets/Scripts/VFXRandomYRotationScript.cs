using UnityEngine;

public class VFXRandomYRotationScript : MonoBehaviour {
    void Start() {
        transform.localRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }
}
