﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model.Creatures {
    public class CreatureQueen : Creature {
        public CreatureQueen() : base("Queen", 100, 5, new CreatureAbilityHome()) { }
    }

    public class CreatureAbilityHome : CreatureAbility {
        public static string NAME = "Home";

        public CreatureAbilityHome() : base(NAME) { }

        public override string GetDescription() {
            return "You can place purchased creatures next to this.";
        }
    }
}
