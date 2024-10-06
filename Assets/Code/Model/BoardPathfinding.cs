using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Code.Model {
    public partial class Board {
        public List<Vector2Int> AStar(Vector2Int from, Vector2Int to) {
            if (!GetTile(to).CanBeMovedTo()) return null;

            Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
            Dictionary<Vector2Int, int> cheapestPaths = new Dictionary<Vector2Int, int>();
            cheapestPaths[from] = 0;
            Dictionary<Vector2Int, int> estimates = new Dictionary<Vector2Int, int>();
            estimates[from] = Util.HexagonalDistance(from, to);
            PriorityQueue<Vector2Int, int> open = new PriorityQueue<Vector2Int, int>();
            open.Enqueue(from, estimates[from]);
            HashSet<Vector2Int> isOpen = new HashSet<Vector2Int>() { from };

            while (open.Count > 0) {
                Vector2Int current = open.Dequeue();
                isOpen.Remove(current);
                if (current == to) break;
                foreach (Vector2Int neighbor in Util.GetNeighboringHexCoors(current)) {
                    if (!GetTile(neighbor).CanBeMovedTo()) {
                        continue;
                    }
                    int tentative = cheapestPaths[current] + 1;
                    if (!cheapestPaths.ContainsKey(neighbor) || tentative < cheapestPaths[neighbor]) {
                        cameFrom[neighbor] = current;
                        cheapestPaths[neighbor] = tentative;
                        estimates[neighbor] = tentative + Util.HexagonalDistance(neighbor, to);
                        if (!isOpen.Contains(neighbor)) {
                            open.Enqueue(neighbor, estimates[neighbor]);
                            isOpen.Add(neighbor);
                        }
                    }
                }
            }

            if (!cameFrom.ContainsKey(to)) {
                return null;
            }
            List<Vector2Int> path = new List<Vector2Int>() { to };
            Vector2Int node = to;
            while (node != from) {
                node = cameFrom[node];
                path.Add(node);
            }
            path.Reverse();
            return path;
        }
    }
}
