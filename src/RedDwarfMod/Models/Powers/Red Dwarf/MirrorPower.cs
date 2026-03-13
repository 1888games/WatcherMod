using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace RedDwarfMod.Models.Powers;


public sealed class MirrorPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public decimal DamageIncoming = 0;
    public Creature? Target;
    public PlayerChoiceContext PlayerChoiceContext;

    public override decimal ModifyHpLostAfterOstyLate(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner)
        {
          
            return amount;
        }

        Target = dealer;

        DamageIncoming = amount;
    

        return 0m;
    }

    public override async Task AfterModifyingHpLostAfterOsty()
    {
        await PowerCmd.Decrement(this);

        Flash();
   
        await CreatureCmd.Damage(PlayerChoiceContext, Target, DamageIncoming, ValueProp.Unpowered | ValueProp.SkipHurtAnim, base.Owner);
    }

    public override async Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target == base.Owner && dealer != null)
        {
            PlayerChoiceContext = choiceContext;
        }
    }
}
