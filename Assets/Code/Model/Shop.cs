using Assets.Code.Model.Creatures;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Shop {
        Game game;
        List<(Creature, float)> weights;
        public List<Creature> creatures;

        public Shop(Game game) {
            this.game = game;
            weights = new List<(Creature, float)>() {
                (Creature.HARVESTER, 100f),
                (Creature.ANGEL, 1000f),
            };
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
            float totalWeight = weights.Select(i => i.Item2).Sum();
            float selector = Random.Range(0, totalWeight);
            foreach (var weight in weights) {
                selector -= weight.Item2;
                if (selector <= 0) {
                    return weight.Item1.Clone();
                }
            }
            throw new System.Exception("Failed to randomly choose a shop creature.");
        }

        public void AddWeight(Creature creature, float weight) {
            weights.Add((creature, weight));
        }
        public void AddSlot() {
            creatures.Add(RandomShopUnit());
        }
    }
}
