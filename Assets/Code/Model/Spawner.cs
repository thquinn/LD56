using Assets.Code.Model.Abilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Spawner : TileFeature {
        int health, cooldown, timer;
        Ability[] abilities;

        public Spawner(Tile tile, int health, int cooldown, params Ability[] abilities) : base(tile) {
            this.health = health;
            this.cooldown = cooldown;
            this.abilities = abilities;
        }

        public override void Tick() {
            timer++;
            if (timer < cooldown) {
                return;
            }
            timer -= cooldown;
            if (tile.CanBeMovedTo()) {
                SpawnEnemy(tile.coor);
                return;
            }
            Tile randomNeighbor = Util.GetNeighboringHexCoors(tile.coor).Select(c => board.GetTile(c)).Where(t => t?.CanBeMovedTo() == true).ToArray().Pick();
            if (randomNeighbor != null) {
                SpawnEnemy(randomNeighbor.coor);
            }
        }
        void SpawnEnemy(Vector2Int coor) {
            board.SpawnEntityAtCoor(new Enemy(health), coor);
        }
    }
}
