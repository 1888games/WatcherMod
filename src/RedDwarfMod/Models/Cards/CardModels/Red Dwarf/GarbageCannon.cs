using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;


public sealed class GarbageCannon() : RedDwarfCardModel(2, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies, RedDwarfCharacter.KRYTEN)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ScrapPower>(),
     
    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(6m, ValueProp.Move),
        

    ];

    public override int ScrapRequiredToPlay => 1;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        ScrapPower scrap = Owner.Creature.GetPower<ScrapPower>();

        float scale = 0.8f;

        await PowerCmd.ModifyAmount(Owner.Creature.GetPower<ScrapPower>(), -scrap.Amount, Owner.Creature, this);

        if (IsUpgraded)
        {

            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(scrap.Amount).FromCard(this)
            .TargetingAllOpponents(CombatState)
            .BeforeDamage(delegate
            {
                NGroundFireVfx nGroundFireVfx = NGroundFireVfx.Create(cardPlay.Target);
                if (nGroundFireVfx == null)
                {
                    return Task.CompletedTask;
                }
                SfxCmd.Play("event:/sfx/characters/attack_fire");
                nGroundFireVfx.Scale = Vector2.One * scale;
                NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(nGroundFireVfx);
                scale += 0.1f;
                return Task.CompletedTask;
            })
            .Execute(choiceContext);

            return;

        }

        await await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(num).FromCard(this)
            .TargetingAllOpponents(base.CombatState)
            .WithHitFx("vfx/vfx_giant_horizontal_slash")
            .Execute(choiceContext);




    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
      

    }
}