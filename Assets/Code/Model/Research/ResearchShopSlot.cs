using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model.Research {
    public class ResearchShopSlot : Research {
        public static string GetID(int cost) { return $"shop_slot_{cost}"; }

        public ResearchShopSlot(int cost) : base(
            GetID(cost),
            "Shop Expansion",
            "Gain an additional slot in the shop.",
            cost
        ) { }

        public override void Unlock(Game game) {
            base.Unlock(game);
            game.shop.AddSlot();
        }
    }
}
