using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code {
    /// <summary>
    /// A counter for animations that should block combat execution.
    /// </summary>
    internal class Pauser {
        int counter;

        public Pauser() {
            counter = 0;
        }

        public bool IsUnpaused() {
            return counter == 0;
        }

        public PauseSource GetSource() {
            return new PauseSource(this);
        }
        public void Increment() {
            counter++;
        }
        public void Decrement() {
            counter--;
        }
    }

    internal class PauseSource {
        Pauser pauser;
        bool on;

        public PauseSource(Pauser pauser) {
            this.pauser = pauser;
            on = false;
        }
        ~PauseSource() {
            Off();
        }

        public void On() {
            if (on) return;
            on = true;
            pauser.Increment();
        }
        public void Off() {
            if (!on) return;
            on = false;
            pauser.Decrement();
        }
        public void Set(bool value) {
            if (value) On(); else Off();
        }
    }
}
