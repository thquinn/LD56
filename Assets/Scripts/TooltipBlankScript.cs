using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipBlankScript : TooltipBehavior
{
    public override IEnumerable<Tooltip> GetTooltips() {
        yield break;
    }
}
