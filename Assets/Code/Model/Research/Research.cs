using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model.Research {
    public abstract class Research {
        public Vector2Int coor;
        public string name, id, description;
        public int cost;
        public List<Research> requirements;
        public bool unlocked;

        public Research(string id, string name, string description, int cost) {
            this.name = name;
            this.id = id;
            this.description = description;
            this.cost = cost;
            requirements = new List<Research>();
            unlocked = false;
        }

        public bool RequirementsMet() {
            return requirements.All(r => r.unlocked);
        }
        public virtual void Unlock(Game game) {
            unlocked = true;
        }
    }

    public class ResearchStart : Research {
        public static string ID = "start";
        public ResearchStart() : base(
            ID,
            "Research",
            "Spend research to find new creatures and gain upgrades.",
            0
        ) {
            unlocked = true;
        }
    }
}
