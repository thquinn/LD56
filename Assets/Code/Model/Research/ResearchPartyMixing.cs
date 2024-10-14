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
            game.researchStatus.partyMixing = true;
        }
    }
}
