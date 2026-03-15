using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedDwarfMod.Models.Powers;

public sealed class CowedPower : PowerModel
{
    private const string _powerAmount = "DamageDecrease";
    private const string _reducePerUse = "ReducePerUse";
    private const string _reducePerTurn = "ReducePerTurn";

    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

  

     protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
       new DynamicVar(_powerAmount, 0.5m),
         new DynamicVar(_reducePerTurn, 10m),
          new DynamicVar(_reducePerUse, 5m),
    ];

    public override decimal ModifyDamageMultiplicative(Creature? target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner)
        {
            return 1m;
        }


        return 1m + (Amount / 100m);
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target == base.Owner)
        {
            Flash();
            await PowerCmd.ModifyAmount(this, -DynamicVars[_reducePerUse].BaseValue, null, null);
        }
    }


    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (side == CombatSide.Enemy) {

            if (Amount == 1)
            {
                await PowerCmd.Decrement(this);
            }
            else
            {

                await PowerCmd.ModifyAmount(this, -Amount / 2, null, null);
            }

        }
           
    }
}
