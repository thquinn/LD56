using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Enemy : Entity {
        public int health;

        public Enemy(int health) {
            this.health = health;
        }

        public override void Act() {
            
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
