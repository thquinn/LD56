using Assets.Code.Model.Creatures;
using Assets.Code.Model.GameEvents;
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
            return tile != null && Util.HexagonalDistance(tile.coor, otherTile.coor) == 1 && otherTile.distanceToRevealed == 1;
        }
        public override bool HasAbility(string name) {
            return creatures.Any(c => c.abilities.Any(a => a.name == name));
        }
        public int GetAttack() {
            return creatures.Select(c => c.GetAttack()).Sum();
        }

        public void MergeIn(Party other) {
            creatures.AddRange(other.creatures);
            other.isDead = true;
            other.tile.entity = null;
        }

        public override void Tick() {
            Attack();
        }
        bool Attack() {
            Enemy[] enemies = tile.GetNeighbors().Select(t => t.entity).Where(e => e is Enemy).Cast<Enemy>().ToArray();
            if (enemies.Length == 0) return false;
            GameEvent filterEvent = new GameEvent() {
                type = GameEventType.AttackFilterTargets,
                source = this,
                enemies = enemies,
            }.Trigger();
            enemies = filterEvent.enemies;
            if (enemies.Length == 0) return false;
            int minHealth = enemies.Min(e => e.health);
            Enemy[] targets = enemies.Where(e => e.health == minHealth).ToArray();
            Enemy target = targets.Pick();
            if (target == null) return false;
            GameEvent damageEvent = new GameEvent() {
                type = GameEventType.AttackBeforeDamage,
                source = this,
                target = target,
                amount = GetAttack(),
            }.Trigger();
            target.Damage(damageEvent.amount);
            if (target.isDead) {
                GameManagerScript.events.Trigger(new GameEvent() {
                    type = GameEventType.EnemyKilled,
                    source = this,
                    target = target,
                });
            }
            return true;
        }
    }
}
