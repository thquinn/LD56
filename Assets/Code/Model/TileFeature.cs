using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public abstract class TileFeature {
        public Board board;
        public Tile tile;

        public TileFeature(Tile tile) {
            board = tile.board;
            this.tile = tile;
        }

        public abstract void Tick();
    }
}
