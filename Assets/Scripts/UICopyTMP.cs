using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICopyTMP : MonoBehaviour
{
    public TextMeshProUGUI tmp, target;
    
    void Update() {
        tmp.text = target.text;
    }
}
