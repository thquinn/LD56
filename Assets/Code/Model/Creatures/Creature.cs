﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public abstract class Creature {
        public Party party;
        public string name;
        public int cost;
        int attack;
        public CreatureAbility[] abilities;

        public Creature(string name, int cost, int attack, params CreatureAbility[] abilities) {
            this.name = name;
            this.cost = cost;
            this.attack = attack;
            this.abilities = abilities;
            foreach (CreatureAbility ability in abilities) {
                ability.AttachTo(this);
            }
        }

        public int GetAttack() {
            return attack;
        }
    }
}
