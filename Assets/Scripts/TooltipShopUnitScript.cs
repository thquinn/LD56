using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipShopUnitScript : TooltipBehavior {
    public UIUnitScript uiUnitScript;

    public override IEnumerable<Tooltip> GetTooltips() {
        if (uiUnitScript.party != null) {
            yield return uiUnitScript.party.GetTooltip();
        }
    }
}
