using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public sealed class GarbagePod() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.RIMMER, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<ScrapPower>(),
      
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [ 
        ..base.CanonicalVars,
        new PowerVar<ScrapPower>(3m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<ScrapPower>(
            Owner.Creature,
            DynamicVars[Scrap].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        DynamicVars[Scrap].UpgradeValueBy(1m);
    }
}