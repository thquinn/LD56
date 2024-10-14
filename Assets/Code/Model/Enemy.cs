using Assets.Code.Model.GameEvents;
using Assets.Scripts;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Code.Model {
    public class Enemy : Entity {
        static int LINGER_START_TICKS = 100, LINGER_CONTINUE_TICKS = 20;

        public int health, maxHealth, armor;
        public EnemyAbility[] abilities;
        public int lingerTimer;

        public Enemy(int health, int armor, params EnemyAbility[] abilities) {
            this.health = health;
            maxHealth = health;
            this.armor = armor;
            this.abilities = abilities;
            lingerTimer = LINGER_START_TICKS;
        }

        public override string GetName() {
            return "Enemy";
        }
        public override Tooltip GetTooltip() {
            return new Tooltip() {
                title = GetName(),
                body = "This is an enemy.",
            };
        }
        public override bool HasAbility(string name) {
            return abilities.Any(a => a.name == name);
        }

        public override void Tick() {
            lingerTimer--;
            if (lingerTimer <= 0) {
                tile.board.game.LoseTime(1);
                lingerTimer += LINGER_CONTINUE_TICKS;
            }
        }
        public void Attacked(Party party, int damage) {
            // Damage changes before armor applied.
            GameEvent attackEvent = new GameEvent() {
                type = GameEventType.AttackBeforeArmor,
                source = party,
                target = this,
                amount = damage,
            }.Trigger();
            damage = attackEvent.amount;
            damage = Mathf.Max(0, damage - armor);
            // Damage changes after armor applied.
            attackEvent = new GameEvent() {
                type = GameEventType.AttackAfterArmor,
                source = party,
                target = this,
                amount = damage,
            }.Trigger();
            damage = attackEvent.amount;
            damage = Mathf.Min(damage, health);
            health -= damage;
            if (health == 0) {
                Die();
            }
        }
    }
}
