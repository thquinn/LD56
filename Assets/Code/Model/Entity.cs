using Assets.Scripts;

namespace Assets.Code.Model {
    public abstract class Entity {
        public Tile tile, tileMovingFrom, tileMovingTo;
        public bool isDead;

        public abstract string GetName();
        public abstract Tooltip GetTooltip();
        public virtual bool CanExplore(Tile tile) { return false; }
        public abstract bool HasAbility(string name);

        public abstract void Tick();
        public void Die() {
            isDead = true;
            tile.entity = null;
            tile = null;
        }
    }
}
