using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Model {
    public abstract class TileFeature {
        public Tile tile;

        public abstract string GetName();
        public abstract string GetDescription();
        public abstract void AfterTick();
    }
}
