using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;

namespace Assets.Code.Model {
    public class Expedition {
        public ExpeditionState state;
        public Party party;
        public HashSet<Tile> selectedTiles, surroundedTiles;
        float lossChance;

        public Expedition() {
            selectedTiles = new HashSet<Tile>();
            surroundedTiles = new HashSet<Tile>();
        }

        public int GetTimeCost() {
            // 1, 2, 2, 3, 3, 3...
            return Mathf.RoundToInt(Mathf.Sqrt(2 * selectedTiles.Count));
        }
        public int GetResearchGain() {
            return selectedTiles.Count + surroundedTiles.Count;
        }
        public float GetLossChance() {
            return Mathf.Clamp01(selectedTiles.Count * .05f - party.GetAttack() * .04f);
        }
        public int GetLossPercent() {
            return Mathf.CeilToInt(GetLossChance() * 100);
        }

        public void FindSurrounded() {
            surroundedTiles.Clear();
            // Surrounded regions must contain at least one tile next to a selected tile.
            HashSet<Tile> seeds = selectedTiles.SelectMany(t => t.GetNeighbors()).ToHashSet();
            seeds.ExceptWith(selectedTiles);
            while (seeds.Count > 0) {
                Tile seed = seeds.First();
                seeds.Remove(seed);
                if (seed.revealed) continue;
                Queue<Tile> queue = new Queue<Tile>();
                queue.Enqueue(seed);
                HashSet<Tile> seen = new HashSet<Tile>() { seed };
                bool hitBorder = false;
                while (queue.Count > 0 && !hitBorder) {
                    Tile current = queue.Dequeue();
                    foreach (Tile neighbor in current.GetNeighbors().Where(t => !t.revealed && !selectedTiles.Contains(t))) {
                        if (neighbor.IsOnBorder()) {
                            hitBorder = true;
                            break;
                        }
                        if (seen.Contains(neighbor)) continue;
                        seen.Add(neighbor);
                        if (seeds.Contains(neighbor)) {
                            seeds.Remove(neighbor);
                        }
                        queue.Enqueue(neighbor);
                    }
                }
                if (!hitBorder) {
                    surroundedTiles.UnionWith(seen);
                }
            }
        }
        public void Start() {
            state = ExpeditionState.Started;
            lossChance = GetLossChance();
            party.tile.MoveEntityHereSlow(party, GetTimeCost());
        }
        public void Finish() {
            state = Random.value < lossChance ? ExpeditionState.Failure : ExpeditionState.Success;
        }
    }

    public enum ExpeditionState {
        Unstarted, Started, Success, Failure
    }
}
