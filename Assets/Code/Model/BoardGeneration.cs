using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public partial class Board {
        static int EXPANSION_CHECK_DISTANCE = 5;
        static int EXPANSION_AMOUNT = 10;

        public void ExpansionCheck() {
            int left = 0, right = 0, up = 0, down = 0;
            for (int x = 0; x < tiles.GetLength(0); x++) {
                for (int y = 0; y < tiles.GetLength(1); y++) {
                    if (!tiles[x, y].revealed) continue;
                    if (x < EXPANSION_CHECK_DISTANCE) left = EXPANSION_AMOUNT;
                    if (x >= tiles.GetLength(0) - EXPANSION_CHECK_DISTANCE) right = EXPANSION_AMOUNT;
                    if (y < EXPANSION_CHECK_DISTANCE) up = EXPANSION_AMOUNT;
                    if (y >= tiles.GetLength(1) - EXPANSION_CHECK_DISTANCE) down = EXPANSION_AMOUNT;
                }
            }
            if (left == 0 && right == 0 && up == 0 && down == 0) {
                return;
            }
            Tile[,] newGrid = new Tile[tiles.GetLength(0) + left + right, tiles.GetLength(1) + up + down];
            for (int x = 0; x < tiles.GetLength(0); x++) {
                for (int y = 0; y < tiles.GetLength(1); y++) {
                    newGrid[x + left, y + up] = tiles[x, y];
                }
            }
            tiles = newGrid;
            centerCoor += new Vector2Int(left, up);
            // Generate new tiles.
            List<Tile> newTiles = new List<Tile>();
            for (int x = 0; x < tiles.GetLength(0); x++) {
                for (int y = 0; y < tiles.GetLength(1); y++) {
                    if (tiles[x, y] == null) {
                        newTiles.Add(CreateTile(x, y));
                    }
                }
            }
            Debug.Assert(NumTiles() == tiles.GetLength(0) * tiles.GetLength(1), "Tile count mismatch after board expansion.");
            PopulateNewTiles(newTiles);
        }
        Tile CreateTile(int xActual, int yActual) {
            Vector2Int actualCoor = new Vector2Int(xActual, yActual);
            Tile tile = new Tile(this, ActualCoorToOrigin(actualCoor));
            tiles[xActual, yActual] = tile;
            tilesByRecent.Add(tile);
            return tile;
        }

        void PopulateNewTiles(List<Tile> newTiles) {
            newTiles.Shuffle();
            int numFeatures = Util.RandRound(newTiles.Where(t => !t.revealed).Count() / 25f);
            foreach (Tile tile in newTiles) {
                // Can't place a feature in an already revealed tile.
                if (tile.revealed) continue;
                // Skip tiles with nearby features.
                if (Util.GetHexCoorsWithinRange(tile.coor, 2).Select(c => GetTile(c)).Any(t => t?.feature != null)) continue;
                PlaceFeatureAtCoor(new Spawner(100, 10), tile.coor);
                numFeatures--;
                if (numFeatures == 0) return;
            }
        }
    }
}
