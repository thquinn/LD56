using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTileScript : TooltipBehavior
{
    public TileScript tileScript;

    public override IEnumerable<Tooltip> GetTooltips() {
        if (tileScript.tile.entity != null) {
            yield return tileScript.tile.entity.GetTooltip();
        }
    }
}
