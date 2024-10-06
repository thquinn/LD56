using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Party : Entity {
        public List<Creature> creatures;

        public Party(params Creature[] creatures) {
            this.creatures = creatures.ToList();
            foreach (Creature creature in creatures) {
                creature.party = this;
            }
        }

        public override string GetName() {
            return string.Join(" & ", creatures.Select(c => c.name));
        }
        public override bool CanExplore(Tile otherTile) {
            return tile != null && Util.HexagonalDistance(tile.coor, otherTile.coor) == 1;
        }
        public override bool HasAbility(string name) {
            return creatures.Any(c => c.abilities.Any(a => a.name == name));
        }
        public int GetAttack() {
            return creatures.Select(c => c.GetAttack()).Sum();
        }

        public override void Tick() {
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
