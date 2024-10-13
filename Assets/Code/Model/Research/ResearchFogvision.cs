namespace Assets.Code.Model.Research {
    public class ResearchFogvision : Research {
        public static string GetID(int cost) { return $"fogvision_{cost}"; }

        public ResearchFogvision(int cost) : base(
            GetID(cost),
            "Fogvision",
            "See an additional tile deeper into the clouds.",
            cost
        ) { }

        public override void Unlock(Game game) {
            base.Unlock(game);
            game.researchStatus.fogVisionRadius++;
        }
    }
}
