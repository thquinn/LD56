using Assets.Code.Model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public abstract class EnemyAbility : Ability {
        public Enemy enemy;

        public EnemyAbility(string name) : base(name) { }
    }
}
