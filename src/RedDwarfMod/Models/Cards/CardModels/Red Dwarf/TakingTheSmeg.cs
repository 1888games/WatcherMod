using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public sealed class TakingTheSmeg() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, RedDwarfCharacter.LISTER)
{

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        
        new("SmegDamage", 6m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        // Get CombatState from the target's CombatState
        var combatState = cardPlay.Target.CombatState;
        var enemy = cardPlay.Target;
        // Count enemies in combat
        if (combatState != null && enemy != null)
        {
            
            var smegPower = enemy.GetPower<SmegPower>();
            if (smegPower != null && smegPower.Amount > 0)
            {
                int amt = smegPower.Amount;

                await PowerCmd.Remove(smegPower);

                // Calculate damage: base damage × enemy count
                var totalDamage =  amt * (int)DynamicVars["SmegDamage"].BaseValue;

                await CreatureCmd.Damage(choiceContext, enemy, totalDamage, ValueProp.Unpowered | ValueProp.SkipHurtAnim | ValueProp.Unblockable,this);

            }
        }
    }

    protected override void OnUpgrade()
    {
       // EnergyCost.UpgradeBy(-1);
        DynamicVars["SmegDamage"].UpgradeValueBy(2m); // 2 → 3
    }
}