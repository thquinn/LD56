using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

namespace Assets.Scripts {
    public abstract class TooltipBehavior : MonoBehaviour {
        public abstract IEnumerable<Tooltip> GetTooltips();
    }

    public struct Tooltip {
        public string title, body;

        public string GetTMPString() {
            string numberPattern = @"\b\d+(\.\d+)?\b(?![^<>]*>)";
            string formattedBody = Regex.Replace(body, numberPattern, "<font=\"Patrician SDF\">$&</font>");
            return $"<font=\"Patrician SDF\"><size=36>{title}</size></font>\n{formattedBody}";
        }
    }
}
