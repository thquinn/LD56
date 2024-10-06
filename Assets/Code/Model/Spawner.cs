using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Spawner : TileFeature {
        int health, cooldown, timer;
        EnemyAbility[] abilities;

        public Spawner(int health, int cooldown, params EnemyAbility[] abilities) {
            this.health = health;
            this.cooldown = cooldown;
            this.abilities = abilities;
        }

        public override void AfterTick() {
            timer++;
            if (timer < cooldown) {
                return;
            }
            timer -= cooldown;
            if (tile.CanBeMovedTo()) {
                SpawnEnemy(tile.coor);
                return;
            }
            Tile randomNeighbor = Util.GetNeighboringHexCoors(tile.coor).Select(c => tile.board.GetTile(c)).Where(t => t?.CanBeMovedTo() == true).ToArray().Pick();
            if (randomNeighbor != null) {
                SpawnEnemy(randomNeighbor.coor);
            }
        }
        void SpawnEnemy(Vector2Int coor) {
            tile.board.SpawnEntityAtCoor(new Enemy(health), coor);
        }
    }
}
