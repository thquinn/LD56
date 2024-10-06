using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace Assets.Code.Model {
    public partial class Board {
        static int STARTING_VISION_RADIUS = 2;

        public Game game;
        Tile[,] tiles;
        List<Tile> tilesByRecent;
        Vector2Int centerCoor;

        public Board(Game game) {
            this.game = game;
            tiles = new Tile[21, 21];
            tilesByRecent = new List<Tile>();
            centerCoor = new Vector2Int(10, 10);
            for (int x = 0; x < tiles.GetLength(0); x++) {
                for (int y = 0; y < tiles.GetLength(1); y++) {
                    CreateTile(x, y);
                }
            }
            RevealTiles(Util.GetHexCoorsWithinRange(Vector2Int.zero, STARTING_VISION_RADIUS).Select(c => GetTile(c)).Where(t => t != null));
            SpawnEntityAtCoor(new Party(new Creature(3)), -Vector2Int.one);
            PlaceFeatureAtCoor(new Spawner(100, 10), new Vector2Int(1, 0));
            PopulateNewTiles(tilesByRecent);
        }

        Vector2Int OriginCoorToActual(Vector2Int originCoor) {
            return originCoor + centerCoor;
        }
        Vector2Int ActualCoorToOrigin(Vector2Int actualCoor) {
            return actualCoor - centerCoor;
        }

        public Tile GetTile(Vector2Int coor) {
            coor = OriginCoorToActual(coor);
            if (coor.x < 0 || coor.y < 0 || coor.x >= tiles.GetLength(0) || coor.y >= tiles.GetLength(1)) {
                return null;
            }
            return tiles[coor.x, coor.y];
        }
        public IEnumerable<Tile> GetTiles() {
            return GetTilesFromIndex(0);
        }
        public IEnumerable<Tile> GetTilesFromIndex(int index) {
            for (int i = index; i < tilesByRecent.Count; i++) {
                yield return tilesByRecent[i];
            }
        }
        public int NumTiles() {
            return tilesByRecent.Count;
        }

        public void RevealTiles(IEnumerable<Tile> tilesToReveal) {
            foreach (Tile tile in tilesToReveal) {
                tile.revealed = true;
                tile.distanceToRevealed = 0;
            }
            // BFS to update fog distance.
            Queue<Tile> queue = new Queue<Tile>();
            queue.Enqueue(GetTile(Vector2Int.zero));
            HashSet<Tile> seen = new HashSet<Tile>() { queue.Peek() };
            while (queue.Count > 0) {
                Tile current = queue.Dequeue();
                foreach (Tile neighbor in current.GetNeighbors()) {
                    if (seen.Contains(neighbor)) continue;
                    neighbor.distanceToRevealed = Mathf.Min(neighbor.distanceToRevealed, current.distanceToRevealed + 1);
                    queue.Enqueue(neighbor);
                    seen.Add(neighbor);
                }
            }
        }

        public void SpawnEntityAtCoor(Entity entity, Vector2Int coor) {
            GetTile(coor).MoveEntityHereImmediate(entity);
        }
        public void PlaceFeatureAtCoor(TileFeature feature, Vector2Int coor) {
            Tile tile = GetTile(coor);
            tile.feature = feature;
            feature.tile = tile;
        }
    }
}
