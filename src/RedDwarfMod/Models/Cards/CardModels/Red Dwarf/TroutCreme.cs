using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public sealed class TroutCreme() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.CAT, true) 
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromPower<SustenancePower>(),
       
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
       

        new PowerVar<SustenancePower>(5m),
        new BlockVar(3m, ValueProp.Move)
      
       
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

        await PowerCmd.Apply<SustenancePower>(
            Owner.Creature,
            DynamicVars[Sustenance].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        DynamicVars[Sustenance].UpgradeValueBy(2m);
        DynamicVars.Block.UpgradeValueBy(2m);

    }
}