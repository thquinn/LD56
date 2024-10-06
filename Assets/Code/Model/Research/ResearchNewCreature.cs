using Assets.Code.Model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model.Research {
    public class ResearchNewCreature : Research {
        public static string GetID(Creature creature) { return $"new_creature_{creature.name.ToLower()}"; }

        Creature creature;
        float weight;

        public ResearchNewCreature(int cost, Creature creature, float weight) : base(
            GetID(creature),
            creature.name,
            "Discover a new creature.",
            cost
        ) {
            this.creature = creature;
            this.weight = weight;
        }

        public override void Unlock(Game game) {
            base.Unlock(game);
            game.shop.AddWeight(creature, weight);
        }
    }
}
