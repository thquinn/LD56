using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXSpawnerScript : MonoBehaviour
{
    static float MAT_DAMP_TIME = 2f;

    public Transform matTransform;

    Vector3 matInitialScale, vMatScale;

    void Start() {
        matInitialScale = matTransform.localScale;
        matTransform.localScale = new Vector3(0, 0, matInitialScale.z);
    }

    void Update() {
        matTransform.localScale = Vector3.SmoothDamp(matTransform.localScale, matInitialScale, ref vMatScale, MAT_DAMP_TIME);
    }
}
