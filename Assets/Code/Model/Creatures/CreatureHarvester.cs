using Assets.Code.Model.GameEvents;

namespace Assets.Code.Model.Creatures {
    public partial class Creature {
        public static Creature HARVESTER = new Creature("Harvester", 25, 3, new CreatureAbilityHarvest());
    }

    public class CreatureAbilityHarvest : CreatureAbility {
        public static string NAME = "Harvest";
        static int AMOUNT = 5;

        public CreatureAbilityHarvest() : base(NAME) { }
        public override Ability Clone() { return new CreatureAbilityHarvest(); }
        public override string GetDescription() {
            return string.Format($"Gain <sprite name=\"money\" tint=1>{AMOUNT} whenever this kills an enemy.");
        }

        public override void AttachTo(Creature creature) {
            base.AttachTo(creature);
            GameManagerScript.events.Listen(
                GameEventType.PartyKilledEnemy,
                e =>
                    IsFirstInstance() &&
                    (e.source as Party)?.creatures.Contains(creature) == true,
            Handle
            );
        }

        bool Handle(GameEvent e) {
            GameManagerScript.instance.game.money += AMOUNT;
            return false;
        }
    }
}
