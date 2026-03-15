using System.Runtime.InteropServices;
using Godot;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.PotionPools;
using RedDwarfMod.Models.CardPools;
using RedDwarfMod.Models.Cards;
using RedDwarfMod.Models.RelicPools;
using RedDwarfMod.Relics;

namespace RedDwarfMod.Models.Characters;

public sealed class Watcher : CharacterModel
{
    public const string energyColorName = "watcher";

    public override CharacterGender Gender => CharacterGender.Feminine;

    protected override CharacterModel? UnlocksAfterRunAs => null;

    public override Color NameColor => StsColors.purple;

    public override int StartingHp => 74;

    public override int StartingGold => 99;

    public override CardPoolModel CardPool => ModelDb.CardPool<WatcherCardPool>();

    public override PotionPoolModel PotionPool => ModelDb.PotionPool<IroncladPotionPool>();

    public override RelicPoolModel RelicPool => ModelDb.RelicPool<WatcherRelicPool>();

    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<StrikeWatcher>(),
        ModelDb.Card<StrikeWatcher>(),
        ModelDb.Card<StrikeWatcher>(),
        ModelDb.Card<StrikeWatcher>(),
        ModelDb.Card<JusticeField>(),
        ModelDb.Card<DefendWatcher>(),
        ModelDb.Card<DefendWatcher>(),
        ModelDb.Card<DefendWatcher>(),
        ModelDb.Card<DefendWatcher>(),
        ModelDb.Card<DistendedRectum>(),
        ModelDb.Card<Smeghead>(),
        ModelDb.Card<Triplicator>(),
        ModelDb.Card<WeldingMallet>(),
        ModelDb.Card<TakingTheSmeg>(),
        ModelDb.Card<MuttonVindaloo>(),
        ModelDb.Card<TroutCreme>(),
        ModelDb.Card<StiffSocks>(),
        ModelDb.Card<CutToenails>(),
        ModelDb.Card<ShootingIrons>(),
        ModelDb.Card<CadmiumTwo>(),
        ModelDb.Card<WhiteHole>(),
        ModelDb.Card<DimensionJump>(),
        ModelDb.Card<ConserveYourEnergy>(),
        ModelDb.Card<RoofAttack>(),
         ModelDb.Card<RoofAttack>(),
            ModelDb.Card<GarbageCannon>(),
            ModelDb.Card<RoofAttack>(),
            ModelDb.Card<GarbageCannon>(),
        ModelDb.Card<WasteDisposal>(),
         ModelDb.Card<GarbagePod>(),





    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<PureWater>()
    ];

    public override float AttackAnimDelay => 0.15f;

    public override float CastAnimDelay => 0.25f;

    public override Color EnergyLabelOutlineColor => new("801212FF");

    public override Color DialogueColor => new("590700");

    public override Color MapDrawingColor => new("CB282B");

    public override Color RemoteTargetingLineColor => new("E15847FF");

    public override Color RemoteTargetingLineOutline => new("801212FF");

    public override List<string> GetArchitectAttackVfx()
    {
        var num = 5;
        var list = new List<string>(num);
        CollectionsMarshal.SetCount(list, num);
        var span = CollectionsMarshal.AsSpan(list);
        var num2 = 0;
        span[num2] = "vfx/vfx_attack_blunt";
        num2++;
        span[num2] = "vfx/vfx_heavy_blunt";
        num2++;
        span[num2] = "vfx/vfx_attack_slash";
        num2++;
        span[num2] = "vfx/vfx_bloody_impact";
        num2++;
        span[num2] = "vfx/vfx_rock_shatter";
        return list;
    }
}