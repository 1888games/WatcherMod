using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Models;
using RedDwarfMod.Models.Cards;

namespace RedDwarfMod.Relics;

public sealed class HolyWater : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Ancient;


    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        var pureWater = this;
        if (side != pureWater.Owner.Creature.Side || combatState.RoundNumber > 1)
            return;

        var miracles =
            new CardModel[]
            {
                combatState.CreateCard<Miracle>(Owner),
                combatState.CreateCard<Miracle>(Owner),
                combatState.CreateCard<Miracle>(Owner)
            };
        await CardPileCmd.AddGeneratedCardsToCombat(miracles, PileType.Hand, true);
        pureWater.Flash();
    }
}