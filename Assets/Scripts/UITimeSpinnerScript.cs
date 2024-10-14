using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeSpinnerScript : MonoBehaviour
{
    public float scaleMin, scaleMax;
    public float spinRateMin, spinRateMax;

    float rate;

    void Start() {
        float scale = Mathf.Lerp(scaleMin, scaleMax, Mathf.InverseLerp(0, transform.parent.childCount - 1, transform.GetSiblingIndex()));
        transform.localScale = new Vector3(scale, scale, 1);
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
        rate = Random.Range(spinRateMin, spinRateMax);
    }

    void Update() {
        transform.Rotate(0, 0, rate * Time.deltaTime);
    }
}
