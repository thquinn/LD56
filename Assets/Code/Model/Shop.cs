using Assets.Code.Model.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public class Shop {
        Game game;
        public List<Creature> creatures;

        public Shop(Game game) {
            this.game = game;
            creatures = new List<Creature>();
            AddSlot();
        }

        public void Buy(int index) {
            game.money -= creatures[index].cost;
            creatures[index] = null;
        }

        public int GetRefreshTickCost() { return 3; }

        public void Refresh() {
            for (int i = 0; i < creatures.Count; i++) {
                creatures[i] = RandomShopUnit();
            }
            game.waitTicks = GetRefreshTickCost();
        }
        Creature RandomShopUnit() {
            return new CreatureHarvester();
        }

        public void AddSlot() {
            creatures.Add(RandomShopUnit());
        }
    }
}
