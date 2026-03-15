
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public sealed class ConserveYourEnergy() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.CAT)
{

    protected override bool HasEnergyCostX => true;

    List<Creature> enemies = new List<Creature>();

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var x = ResolveEnergyXValue();

        if (IsUpgraded)
            x += 1;

        await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature, x, base.Owner.Creature, this);
        PlayerCmd.EndTurn(Owner, false);

    }

    
}