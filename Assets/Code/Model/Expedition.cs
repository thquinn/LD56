using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Expedition {
        public Party party;
        public HashSet<Tile> tiles;

        public Expedition() {
            tiles = new HashSet<Tile>();
        }

        public int GetTimeCost() {
            // 1, 2, 2, 3, 3, 3...
            return Mathf.RoundToInt(Mathf.Sqrt(2 * tiles.Count));
        }
        public int GetResearchGain() {
            return tiles.Count;
        }
        public float GetLossChance() {
            return Mathf.Clamp01(tiles.Count * .05f - party.GetAttack() * .04f);
        }
        public int GetLossPercent() {
            return Mathf.CeilToInt(GetLossChance() * 100);
        }
    }
}
