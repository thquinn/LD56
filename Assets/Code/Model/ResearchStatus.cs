using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public struct ResearchStatus {
        public int partySize;
        public bool partyMixing;
        public int fogVisionRadius;

        public static ResearchStatus STARTING_STATUS = new ResearchStatus() {
            partySize = 1,
            partyMixing = false,
            fogVisionRadius = 2,
        };
    }
}
