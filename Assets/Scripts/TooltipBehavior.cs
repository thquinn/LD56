﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts {
    public abstract class TooltipBehavior : MonoBehaviour {
        public abstract List<string> GetTooltip();
    }
}
