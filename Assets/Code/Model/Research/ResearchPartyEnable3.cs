namespace Assets.Code.Model.Research {
    public class ResearchPartyEnable3 : Research {
        public static string ID = "party_enable3";
        public ResearchPartyEnable3() : base(
            ID,
            "Party of Three",
            "Combine creatures into parties of three.",
            50
        ) { }

        public override void Unlock(Game game) {
            base.Unlock(game);
            game.researchStatus.partySize++;
        }
    }
}
