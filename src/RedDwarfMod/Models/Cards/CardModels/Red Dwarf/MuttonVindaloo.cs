using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public sealed class MuttonVindaloo() : RedDwarfCardModel(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.LISTER, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<SustenancePower>(),
      
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<SustenancePower>(3m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<SustenancePower>(
            Owner.Creature,
            DynamicVars["SustenancePower"].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        DynamicVars["SustenancePower"].UpgradeValueBy(1m);
    }
}