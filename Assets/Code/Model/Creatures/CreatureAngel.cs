using Assets.Code.Model.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.SocialPlatforms;

namespace Assets.Code.Model.Creatures {
    public partial class Creature {
        public static Creature ANGEL = new Creature("Angel", 20, 5, new CreatureAbilityMercy());
    }

    public class CreatureAbilityMercy : CreatureAbility {
        public static string NAME = "Mercy";

        public CreatureAbilityMercy() : base(NAME) { }
        public override Ability Clone() { return new CreatureAbilityMercy(); }
        public override string GetDescription() {
            return string.Format($"Won't deal the last point of damage to an enemy.");
        }

        public override void AttachTo(Creature creature) {
            base.AttachTo(creature);
            GameManagerScript.events.Listen(
                GameEventType.AttackFilterTargets,
                e =>
                    (e.source as Party)?.creatures.Contains(creature) == true &&
                    e.enemies.Any(enemy => enemy.health == 1),
                HandleAttackFilterTargets
            );
            GameManagerScript.events.Listen(
                GameEventType.AttackBeforeDamage,
                e => (e.source as Party)?.creatures.Contains(creature) == true,
                HandleAttackBeforeDamage
            );
        }

        bool HandleAttackFilterTargets(GameEvent e) {
            e.enemies = e.enemies.Where(enemy => enemy.health > 1).ToArray();
            return true;
        }
        bool HandleAttackBeforeDamage(GameEvent e) {
            Enemy enemy = e.target as Enemy;
            if (e.amount >= enemy.health) {
                e.amount = enemy.health - 1;
                return true;
            }
            return false;
        }
    }
}
