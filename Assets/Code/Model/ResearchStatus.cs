namespace Assets.Code.Model {
    public struct ResearchStatus {
        public int partySize;
        public bool partyMixing;
        public int fogVisionRadius;

        public static ResearchStatus STARTING_STATUS = new ResearchStatus() {
            partySize = 1,
            partyMixing = false,
            fogVisionRadius = 1,
        };
    }
}
