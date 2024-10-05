using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public class Game {
        public int ticks;
        public Board board;

        public Game() {
            ticks = 100;
            board = new Board();
        }

        public void Tick() {
            foreach (Entity entity in board.GetTiles().Select(t => t.entity).Where(e => e != null).ToArray().Shuffle()) {
                entity.Act();
            }
            foreach (Tile tile in board.GetTiles()) {
                tile.Tick();
            }
        }
    }
}
