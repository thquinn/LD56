using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public class TooltipBlankScript : TooltipBehavior {
        public override IEnumerable<Tooltip> GetTooltips() {
            yield break;
        }
    }
}
