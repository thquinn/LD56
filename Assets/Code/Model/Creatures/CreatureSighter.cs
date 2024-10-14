using Assets.Code.Model.GameEvents;
using System.Linq;

namespace Assets.Code.Model.Creatures {
    public partial class Creature {
        public static Creature SIGHTER = new Creature("Sighter", 20, 2, new CreatureAbilityRangefind());
    }

    public class CreatureAbilityRangefind : CreatureAbility {
        public static string NAME = "Rangefind";

        public CreatureAbilityRangefind() : base(NAME) { }
        public override Ability Clone() { return new CreatureAbilityRangefind(); }
        public override string GetDescription() {
            return string.Format($"Can attack enemies an additional tile away.");
        }

        public override void AttachTo(Creature creature) {
            base.AttachTo(creature);
            GameManagerScript.events.Listen(
                GameEventType.CalculateRange,
                ShouldHandleEventAsSource,
                HandleCalculateRange
            );
        }

        bool HandleCalculateRange(GameEvent e) {
            e.amount++;
            return true;
        }
    }
}
