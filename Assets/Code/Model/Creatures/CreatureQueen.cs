using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model.Creatures {
    public partial class Creature {
        public static Creature QUEEN = new Creature("Queen", 100, 5, new CreatureAbilityHome());
    }

    public class CreatureAbilityHome : CreatureAbility {
        public static string NAME = "Home";

        public CreatureAbilityHome() : base(NAME) { }
        public override Ability Clone() { return new CreatureAbilityHome(); }
        public override string GetDescription() {
            return "You can place purchased creatures next to this.";
        }
    }
}
