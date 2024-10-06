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
        public Entity entity, entityMovingFrom, entityMovingTo;
        public TileFeature feature;
        public bool featureVisibleInFog;
        public int moveTicksLeft;

        public Tile(Board board, Vector2Int coor) {
            this.board = board;
            this.coor = coor;
        }

        public IEnumerable<Tile> GetNeighbors() {
            foreach (Vector2Int neighborCoor in Util.GetNeighboringHexCoors(coor)) {
                Tile neighbor = board.GetTile(neighborCoor);
                if (neighbor != null) yield return neighbor;
            }
        }

        public bool CanBeMovedTo() {
            return revealed && entity == null && entityMovingFrom == null && entityMovingTo == null;
        }

        public void MoveEntityHereImmediate(Entity entity) {
            if (entity.tile != null) entity.tile.entity = null;
            entity.tile = this;
            this.entity = entity;
        }
        public void MoveEntityHereSlow(Entity entity, int ticks) {
            Debug.Assert(entity.tile != null, "Entities shouldn't be slow moving unless they're already on the board.");
            entity.tile.entityMovingFrom = entity;
            entity.tile.entity = null;
            entity.tileMovingFrom = entity.tile;
            entity.tile = null;
            entity.tileMovingTo = this;
            entityMovingTo = entity;
            moveTicksLeft = ticks;
        }

        public void Tick() {
            if (entityMovingTo != null) {
                moveTicksLeft--;
                if (moveTicksLeft == 0) {
                    entity = entityMovingTo;
                    entity.tile = this;
                    entityMovingTo = null;
                    entity.tileMovingFrom.entityMovingFrom = null;
                    entity.tileMovingFrom = null;
                    entity.tileMovingTo = null;
                }
            }
            feature?.Tick();
        }
    }
}
