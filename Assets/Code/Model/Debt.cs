using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Model {
    public class Debt {
        static int[] PAYMENTS = new int[] { 100, 200, 500, 1000, 2500, 5000, 10000 };
        static int RESET_TIME = 100;

        Game game;
        public int current;
        int payments;

        public Debt(Game game) {
            this.game = game;
            current = PAYMENTS[0];
        }

        public void Pay() {
            if (game.money >= current) {
                game.money -= current;
                game.time = RESET_TIME;
                payments++;
                current = PAYMENTS[Mathf.Min(payments, PAYMENTS.Length - 1)];
            } else {
                game.gameOver = true;
            }
        }
    }
}
