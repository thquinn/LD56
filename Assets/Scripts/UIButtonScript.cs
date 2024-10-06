using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonScript : MonoBehaviour
{
    static float DAMP_TIME = .05f;
    static Vector3 HOVERED_SCALE_BACK = new Vector3(1.1f, 1.1f, 1);
    static Vector3 HOVERED_SCALE_FILL = new Vector3(1.2f, 1.2f, 1);

    public RectTransform rtBack, rtFill;
    public Image imageBack;
    public Color colorClick;

    bool hovered = false;
    Vector3 vScaleBack, vScaleFill;
    Color backInitialColor;
    float backColorT, vBackColorT;

    void Start() {
        backInitialColor = imageBack.color;
    }

    void Update() {
        rtBack.localScale = Vector3.SmoothDamp(rtBack.localScale, hovered ? HOVERED_SCALE_BACK : Vector3.one, ref vScaleBack, DAMP_TIME);
        rtFill.localScale = Vector3.SmoothDamp(rtFill.localScale, hovered ? HOVERED_SCALE_FILL : Vector3.one, ref vScaleFill, DAMP_TIME);
        backColorT = Mathf.SmoothDamp(backColorT, 0, ref vBackColorT, DAMP_TIME);
        imageBack.color = Color.Lerp(backInitialColor, colorClick, backColorT);
    }

    public void MouseEnter() {
        hovered = true;
    }
    public void MouseLeave() {
        hovered = false;
    }
    public void MouseDown() {
        imageBack.color = colorClick;
        backColorT = 1;
        vBackColorT = 0;
    }
}
