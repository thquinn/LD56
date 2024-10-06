using Assets.Code.Model.Creatures;
using Assets.Code.Model.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public partial class Game {
        public GameEventManager gameEventManager;
        public Board board;
        public int time, money, researchPoints;
        public int waitTicks;
        public Expedition expedition;
        public ResearchStatus researchStatus;
        public Shop shop;

        public Game() {
            gameEventManager = new GameEventManager();
            
        }
        public void Init() {
            time = 100;
            researchStatus = ResearchStatus.STARTING_STATUS;
            board = new Board(this);
            shop = new Shop(this);
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
