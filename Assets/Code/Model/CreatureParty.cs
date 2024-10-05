using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class CreatureParty : Entity {
        public List<Creature> creatures;

        public CreatureParty(Creature creature) {
            creatures = new List<Creature>() { creature };
            creature.party = this;
        }

        public int GetAttack() {
            return creatures.Select(c => c.GetAttack()).Sum();
        }

        public override void Act() {
            Attack();
        }
        bool Attack() {
            var enemies = tile.GetNeighbors().Select(t => t.entity).Where(e => e is Enemy).Cast<Enemy>();
            if (!enemies.Any()) return false;
            int minHealth = enemies.Min(e => e.health);
            Enemy target = enemies.Where(e => e.health == minHealth).ToArray().Pick();
            if (target == null) return false;
            target.Damage(GetAttack());
            return true;
        }
    }
}
