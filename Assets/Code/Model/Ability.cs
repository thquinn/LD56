using Assets.Code.Model.Creatures;

namespace Assets.Code.Model {
    public abstract class Ability {
        public string name;

        public Ability(string name) {
            this.name = name;
        }
        public abstract Ability Clone();
        public abstract string GetDescription();
    }

    public abstract class CreatureAbility : Ability {
        public Creature creature;

        public CreatureAbility(string name) : base(name) { }
        public virtual void AttachTo(Creature creature) {
            this.creature = creature;
        }
        protected bool IsFirstInstance() {
            if (creature?.party == null) return false;
            foreach (Ability ability in creature.party.GetAbilities()) {
                if (ability.name == name) return ability == this;
            }
            return false;
        }
    }

    public abstract class EnemyAbility : Ability {
        public Enemy enemy;

        public EnemyAbility(string name) : base(name) { }
    }
}
