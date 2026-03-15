using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using RedDwarfMod.Models.Cards;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Relics;

public sealed class NovaFive : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;


    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        var nova5 = this;
        if (side != nova5.Owner.Creature.Side || combatState.RoundNumber > 1)
            return;

        ScrapPower power = await PowerCmd.Apply<ScrapPower>(Owner.Creature, 2, Owner.Creature, null);

        nova5.Flash();
    }
}