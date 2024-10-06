using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public override bool HasAbility(string name) {
            return abilities.Any(a => a.name == name);
        }

        public override void Tick() {
            
        }
        public void Damage(int amount) {
            amount = Mathf.Min(amount, health);
            health -= amount;
            if (health == 0) {
                Die();
            }
        }
    }
}
