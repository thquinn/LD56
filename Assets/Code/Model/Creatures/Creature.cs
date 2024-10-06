using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public class Creature {
        public Party party;
        int attack;

        public Creature(int attack) {
            this.attack = attack;
        }

        public int GetAttack() {
            return attack;
        }
    }
}
