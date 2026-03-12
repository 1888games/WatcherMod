using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using RedDwarfMod.Models.Stances;

namespace RedDwarfMod.Relics;

public sealed class TeardropLocket : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;


    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        var locket = this;
        if (side != locket.Owner.Creature.Side || combatState.RoundNumber > 1)
            return;

        await ChangeStanceCmd.Execute(Owner.Creature, ModelDb.Power<CalmStance>(), null);
        locket.Flash();
    }
}