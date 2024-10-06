using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model.Research {
    public class ResearchPartyMixing : Research {
        public static string ID = "party_mixing";
        public ResearchPartyMixing() : base(
            ID,
            "Variety",
            "Combine different creatures into parties.",
            20
        ) { }

        public override void Unlock(Game game) {
            base.Unlock(game);
            game.researchStatus.partySize++;
        }
    }
}
