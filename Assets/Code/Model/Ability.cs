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
    }

    public abstract class CreatureAbility : Ability {
        public Creature creature;

        public CreatureAbility(string name) : base(name) { }
    }

    public abstract class EnemyAbility : Ability {
        public Enemy enemy;

        public EnemyAbility(string name) : base(name) { }
    }
}
