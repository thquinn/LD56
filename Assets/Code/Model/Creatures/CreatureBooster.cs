namespace Assets.Code.Model.Creatures {
    public partial class Creature {
        public static Creature BOOSTER = new Creature("Booster", 30, 0, new CreatureAbilityBoost());
    }

    public class CreatureAbilityBoost : CreatureAbility {
        public static string NAME = "Boost";

        public CreatureAbilityBoost() : base(NAME) { }
        public override Ability Clone() { return new CreatureAbilityBoost(); }
        public override string GetDescription() {
            return "Doubles the attack power of other creatures in its party.";
        }
    }
}
