using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model.Research {
    public class ResearchPartyEnable : Research {
        public static string ID = "party_enable";
        public ResearchPartyEnable() : base(
            ID,
            "Banding",
            "Combine similar creatures into parties of two.",
            5
        ) { }

        public override void Unlock(Game game) {
            base.Unlock(game);
            game.researchStatus.partySize++;
        }
    }
}
