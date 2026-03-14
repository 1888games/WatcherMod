using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using RedDwarfMod.Commands;
using RedDwarfMod.Models.Stances;

namespace RedDwarfMod.Models.Powers;

public sealed class SustenancePower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterApplied(Creature? applier, CardModel? cardSource)
    {
        // Subscribe to the Owner's PowerIncreased event
        Owner.PowerIncreased += OnPowerIncreased;

        // Check immediately in case we start with 10+
        await CheckForEnergy();
    }

    public override Task AfterRemoved(Creature oldOwner)
    {
        // Unsubscribe when removed
        oldOwner.PowerIncreased -= OnPowerIncreased;
        return Task.CompletedTask;
    }

    private async void OnPowerIncreased(PowerModel power, int change, bool silent)
    {
        // Only react to our own increases
        if (power == this && change > 0) await CheckForEnergy();
    }

    private async Task CheckForEnergy()
    {
        // Check if we have 10 or more Mantra
        while (Amount >= 10)
        {
            var player = Owner.Player;
            if (player != null)
            {
                await PlayerCmd.GainEnergy(2, player);
                await PowerCmd.ModifyAmount(this, -10m, null, null);
            }
            else
            {
                break;
            }
        }
    }
}