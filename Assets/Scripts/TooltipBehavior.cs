using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts {
    public abstract class TooltipBehavior : MonoBehaviour {
        public abstract IEnumerable<Tooltip> GetTooltips();
    }

    public struct Tooltip {
        public string title, body;

        public string GetTMPString() {
            return $"<font=\"Patrician SDF\"><size=36>{title}</size></font>\n{body}";
        }
    }
}
