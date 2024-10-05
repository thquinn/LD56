using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Tile {
        public Board board;
        public Vector2Int coor;
        public bool revealed;
        public Entity entity;

        public Tile(Board board, Vector2Int coor) {
            this.board = board;
            this.coor= coor;
        }

        public void MoveEntityHere(Entity entity) {
            if (entity.tile != null) entity.tile.entity = null;
            entity.tile = this;
            this.entity = entity;
        }
    }
}
