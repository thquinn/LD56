using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public class CreatureParty : Entity {
        public List<Creature> creatures;

        public CreatureParty(Creature creature) {
            creatures = new List<Creature>() { creature };
        }
    }
}
