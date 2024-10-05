using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public abstract class Entity {
        public Tile tile, tileMovingFrom, tileMovingTo;
        public bool isDead;

        public abstract void Tick();
        public void Die() {
            isDead = true;
            tile.entity = null;
            tile = null;
        }
    }
}
