using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public class Game {
        public Board board;
        public int time, money, researchPoints;
        public int waitTicks;
        public Expedition expedition;
        public ResearchStatus researchStatus;

        public Game() {
            researchStatus = ResearchStatus.STARTING_STATUS;
            board = new Board(this);
            time = 100;
        }

        public void Tick() {
            foreach (Entity entity in board.GetTiles().Select(t => t.entity).Where(e => e != null).ToArray().Shuffle()) {
                entity.Tick();
            }
            time--;
            if (waitTicks > 0) waitTicks--;
            foreach (Tile tile in board.GetTiles()) {
                tile.AfterTick();
            }
            if (waitTicks == 0) {
                if (expedition != null) {
                    board.RevealTiles(expedition.tiles);
                    researchPoints += expedition.GetResearchGain();
                    expedition = null;
                }
            }
            board.ExpansionCheck();
        }

        public void StartExpedition(Expedition expedition) {
            this.expedition = expedition;
            waitTicks = expedition.GetTimeCost();
        }
    }
}
