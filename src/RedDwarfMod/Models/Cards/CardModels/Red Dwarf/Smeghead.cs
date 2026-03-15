using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public sealed class Smeghead() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.LISTER)
{


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
         new PowerVar<CowedPower>(25m),
         new PowerVar<InsultPower>(1m)
        
    ];

  
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
         ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<CowedPower>(),
        HoverTipFactory.FromPower<InsultPower>(),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        // Apply Mark power
        await PowerCmd.Apply<CowedPower>(
            cardPlay.Target,
            DynamicVars[Cowed].IntValue,
            Owner.Creature,
            this
        );
       
    }

    protected override void OnUpgrade()
    {
        DynamicVars[Cowed].UpgradeValueBy(10m);
    }
}