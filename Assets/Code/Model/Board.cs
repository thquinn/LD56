using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Board {
        Tile[,] tiles;
        List<Tile> tilesByRecent;
        Vector2Int centerCoor;

        public Board() {
            tiles = new Tile[21, 21];
            tilesByRecent = new List<Tile>();
            centerCoor = new Vector2Int(10, 10);
            for (int x = 0; x < tiles.GetLength(0); x++) {
                for (int y = 0; y < tiles.GetLength(1); y++) {
                    Vector2Int actualCoor = new Vector2Int(x, y);
                    Tile tile = new Tile(this, ActualCoorToOrigin(actualCoor));
                    if (Util.HexagonalDistance(actualCoor, centerCoor) <= 2) {
                        tile.revealed = true;
                    }
                    tiles[x, y] = tile;
                    tilesByRecent.Add(tile);
                }
            }
            SpawnEntityAtCoor(new CreatureParty(new Creature()), Vector2Int.zero);
        }

        Vector2Int OriginCoorToActual(Vector2Int originCoor) {
            return originCoor + centerCoor;
        }
        Vector2Int ActualCoorToOrigin(Vector2Int originCoor) {
            return centerCoor - originCoor;
        }

        public Tile GetTile(Vector2Int coor) {
            coor = OriginCoorToActual(coor);
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

        public void SpawnEntityAtCoor(Entity entity, Vector2Int coor) {
            GetTile(coor).MoveEntityHere(entity);
        }
    }
}
