using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model.Features {
    public class Ore : TileFeature {
        Game game;
        int timer, minTime, maxTime;

        public Ore(int minTime, int maxTime) {
            game = GameManagerScript.instance.game;
            this.minTime = minTime;
            this.maxTime = maxTime;
            timer = maxTime;
        }

        public override string GetName() {
            return "Ore";
        }
        public override string GetDescription() {
            return $"Gives you <sprite name=\"money\" tint=1> 1 every <sprite name=\"time\" tint=1>{minTime}-{maxTime}.";
        }

        public override void AfterTick() {
            timer--;
            if (timer == 0) {
                game.money++;
                timer = Random.Range(minTime, maxTime + 1);
            }
        }
    }
}
