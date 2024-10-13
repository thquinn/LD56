namespace Assets.Code.Model {
    public abstract class TileFeature {
        public Tile tile;

        public abstract string GetName();
        public abstract string GetDescription();
        public abstract void AfterTick();
    }
}
