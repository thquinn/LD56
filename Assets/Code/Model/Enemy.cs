using Assets.Scripts;
using System.Linq;
using UnityEngine;

namespace Assets.Code.Model {
    public class Enemy : Entity {
        public int health, maxHealth;
        public EnemyAbility[] abilities;

        public Enemy(int health, params EnemyAbility[] abilities) {
            this.health = health;
            maxHealth = health;
            this.abilities = abilities;
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

        }
        public void Damage(int amount) {
            amount = Mathf.Min(amount, health);
            health -= amount;
            if (health == 0) {
                tile.board.game.time++;
                Die();
            }
        }
    }
}
