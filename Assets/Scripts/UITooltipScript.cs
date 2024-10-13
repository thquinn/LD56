using Assets.Code;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltipScript : MonoBehaviour
{
    public LayerMask tooltipMask;
    public TextMeshProUGUI tmp;
    public CanvasGroup researchScreenCanvasGroup;

    int layerTooltippable;

    void Start() {
        layerTooltippable = LayerMask.NameToLayer("UI Tooltippable");
    }

    void Update() {
        tmp.color = Color.Lerp(Color.black, Color.white, researchScreenCanvasGroup.alpha);
        TooltipBehavior tooltipBehavior = GetHoveredBehavior();
        if (tooltipBehavior == null) {
            tmp.text = "";
            return;
        }
        IEnumerable<Tooltip> tooltips = tooltipBehavior.GetTooltips();
        tmp.text = string.Join("\n\n", tooltips.Select(t => t.GetTMPString()));
    }

    TooltipBehavior GetHoveredBehavior() {
        // UI raycast.
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (RaycastResult result in raycastResults) {
            if (result.gameObject.layer == layerTooltippable) {
                return result.gameObject.GetComponent<TooltipBehavior>();
            }
        }
        // World raycast.
        Collider c = Util.GetMouseCollider(tooltipMask);
        return c?.GetComponent<TooltipBehavior>();
    }
}
