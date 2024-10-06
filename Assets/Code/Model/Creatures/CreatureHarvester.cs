using Assets.Code.Model.GameEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.UI.CanvasScaler;
using UnityEngine.SocialPlatforms;

namespace Assets.Code.Model.Creatures {
    public class CreatureHarvester : Creature {
        public CreatureHarvester() : base("Harvester", 25, 5, new CreatureAbilityHarvest()) { }
    }

    public class CreatureAbilityHarvest : CreatureAbility {
        public static string NAME = "Harvest";
        static int AMOUNT = 5;

        public CreatureAbilityHarvest() : base(NAME) { }

        public override void AttachTo(Creature creature) {
            base.AttachTo(creature);
            GameManagerScript.events.Listen(
                GameEventType.EnemyKilled,
                e => (e.source as Party)?.creatures.Contains(creature) == true,
            Handle
            );
        }

        public override string GetDescription() {
            return string.Format($"Gain <sprite name=\"money\" tint=1>{AMOUNT} whenever this kills an enemy.");
        }

        bool Handle(GameEvent e) {
            GameManagerScript.instance.game.money += AMOUNT;
            return false;
        }
    }
}
