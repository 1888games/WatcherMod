using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public sealed class StiffSocks() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.LISTER)
{


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
         new PowerVar<SmegPower>(1m),
         

    ];

  
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
         ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<SmegPower>(),
        HoverTipFactory.FromPower<DisgustPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        // Apply Mark power
        await PowerCmd.Apply<SmegPower>(
            cardPlay.Target,
            DynamicVars["SmegPower"].IntValue,
            Owner.Creature,
            this
        );
       
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}