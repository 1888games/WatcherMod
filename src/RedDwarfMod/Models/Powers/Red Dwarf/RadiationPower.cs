using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedDwarfMod.Models.Powers;


public sealed class RadiationPower : PowerModel
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override Color AmountLabelColor => PowerModel._normalAmountLabelColor;

    private int TriggerCount = 1;
    

    public int CalculateTotalDamageNextTurn()
    {
        decimal num = default(decimal);
        int num2 = Math.Min(base.Amount, TriggerCount);
        for (int i = 0; i < num2; i++)
        {
            decimal damage = base.Amount - i;
            damage = Hook.ModifyDamage(base.Owner.CombatState.RunState, base.Owner.CombatState, base.Owner, null, damage, ValueProp.Unblockable | ValueProp.Unpowered, null, ModifyDamageHookType.All, CardPreviewMode.None, out IEnumerable<AbstractModel> _);
            num += damage;
        }
        return (int)num;
    }

    public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side != base.Owner.Side)
        {
            return;
        }
        int iterations = TriggerCount;
        for (int i = 0; i < iterations; i++)
        {
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), base.Owner, base.Amount, ValueProp.Unblockable | ValueProp.Unpowered, null, null);
            if (base.Owner.IsAlive)
            {
                if (Amount == 1)
                {
                    await PowerCmd.Decrement(this);
                }
                else
                {

                    await PowerCmd.ModifyAmount(this, -Amount / 2, null, null);
                }
                
            }
            else
            {
                await Cmd.CustomScaledWait(0.1f, 0.25f);
            }
        }
    }
}