using Assets.Code.Model.Creatures;
using Assets.Code.Model.GameEvents;
using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;

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
        public override Tooltip GetTooltip() {
            return new Tooltip() {
                title = Util.StringJoinCommaAmpersand(creatures.Select(c => c.name).ToArray()),
                body = string.Join('\n', GetAbilities().Select(a => $"<b>{a.name}</b>    {a.GetDescription()}")),
            };
        }
        public override bool CanExplore(Tile otherTile) {
            return tile != null && Util.HexagonalDistance(tile.coor, otherTile.coor) == 1 && otherTile.distanceToRevealed == 1;
        }
        public bool Contains(Creature creature) {
            return creatures.Contains(creature);
        }
        public override bool HasAbility(string name) {
            return creatures.Any(c => c.abilities.Any(a => a.name == name));
        }
        public IEnumerable<Ability> GetAbilities() {
            HashSet<string> abilityNames = new HashSet<string>();
            foreach (Creature creature in creatures) {
                foreach (Ability ability in creature.abilities) {
                    if (!abilityNames.Contains(ability.name)) {
                        yield return ability;
                        abilityNames.Add(ability.name);
                    }
                }
            }
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
            GameEvent rangeEvent = new GameEvent() {
                type = GameEventType.CalculateRange,
                source = this,
                amount = 1,
            }.Trigger();
            Enemy[] enemies = tile.GetTilesWithin(rangeEvent.amount).Select(t => t.entity).Where(e => e is Enemy).Cast<Enemy>().ToArray();
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
            target.Attacked(this, GetAttack());
            if (target.isDead) {
                GameManagerScript.events.Trigger(new GameEvent() {
                    type = GameEventType.PartyKilledEnemy,
                    source = this,
                    target = target,
                });
            }
            return true;
        }
    }
}
