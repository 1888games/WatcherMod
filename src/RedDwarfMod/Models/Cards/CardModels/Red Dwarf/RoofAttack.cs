using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;


public sealed class RoofAttack() : RedDwarfCardModel(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.LISTER)
{


    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        ..base.ExtraHoverTips,
        HoverTipFactory.FromPower<ScrapPower>(),
        HoverTipFactory.FromPower<LootPower>(),

    ];


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
        new DamageVar(7m, ValueProp.Move),
        new PowerVar<ScrapPower>(1m)

    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);
        await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_attack_slash")
            .Execute(choiceContext);

        Vector2? monsterPos = NCombatRoom.Instance.GetCreatureNode(cardPlay.Target)?.VfxSpawnPosition;

        if (monsterPos.HasValue)
        {
            VfxCmd.PlayVfx(monsterPos.Value, "vfx/vfx_coin_explosion_regular");
        }

        await PowerCmd.Apply<ScrapPower>(
            Owner.Creature,
            DynamicVars[Scrap].IntValue,
            Owner.Creature,
            this
        );

    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
        DynamicVars[Scrap].UpgradeValueBy(1m);

    }
}