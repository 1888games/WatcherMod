using Godot;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Unlocks;
using RedDwarfMod.Models.Cards;

namespace RedDwarfMod.Models.CardPools;

public sealed class WatcherCardPool : CardPoolModel
{
    public override string Title => "watcher";

    public override string EnergyColorName => "watcher";

    public override string CardFrameMaterialPath => "card_frame_purple";

    public override Color DeckEntryCardColor => new("3EB3ED");

    public override Color EnergyOutlineColor => new("1D5673");

    public override bool IsColorless => false;

    protected override CardModel[] GenerateAllCards()
    {
        return
        [
            ModelDb.Card<DefendWatcher>(),
            ModelDb.Card<StrikeWatcher>(),
            ModelDb.Card<Vigilance>(),
            ModelDb.Card<Eruption>(),
            ModelDb.Card<BowlingBash>(),
            ModelDb.Card<Consecrate>(),
            ModelDb.Card<Crescendo>(),
            ModelDb.Card<CrushJoints>(),
            ModelDb.Card<CutThroughFate>(),
            ModelDb.Card<EmptyBody>(),
            ModelDb.Card<EmptyFist>(),
            ModelDb.Card<Evaluate>(),
            ModelDb.Card<FlurryOfBlows>(),
            ModelDb.Card<FlyingSleeves>(),
            ModelDb.Card<FollowUp>(),
            ModelDb.Card<Halt>(),
            ModelDb.Card<JustLucky>(),
            ModelDb.Card<PressurePoints>(),
            ModelDb.Card<Prostrate>(),
            ModelDb.Card<Protect>(),
            ModelDb.Card<SashWhip>(),
            ModelDb.Card<ThirdEye>(),
            ModelDb.Card<Tranquility>(),
            ModelDb.Card<BattleHymn>(),
            ModelDb.Card<CarveReality>(),
            ModelDb.Card<Collect>(),
            ModelDb.Card<Conclude>(),
            ModelDb.Card<DeceiveReality>(),
            ModelDb.Card<Fasting>(),
            ModelDb.Card<FearNoEvil>(),
            ModelDb.Card<ForeignInfluence>(),
            ModelDb.Card<Foresight>(),
            ModelDb.Card<Indignation>(),
            ModelDb.Card<InnerPeace>(),
            ModelDb.Card<LikeWater>(),
            ModelDb.Card<Meditate>(),
            ModelDb.Card<MentalFortress>(),
            ModelDb.Card<Nirvana>(),
            ModelDb.Card<Perseverance>(),
            ModelDb.Card<Pray>(),
            ModelDb.Card<ReachHeaven>(),
            ModelDb.Card<Rushdown>(),
            ModelDb.Card<Sanctity>(),
            ModelDb.Card<SandsOfTime>(),
            ModelDb.Card<SignatureMove>(),
            ModelDb.Card<SimmeringFury>(),
            ModelDb.Card<Study>(),
            ModelDb.Card<Swivel>(),
            ModelDb.Card<TalkToTheHand>(),
            ModelDb.Card<Tantrum>(),
            ModelDb.Card<Wallop>(),
            ModelDb.Card<WaveOfTheHand>(),
            ModelDb.Card<Weave>(),
            ModelDb.Card<WheelKick>(),
            ModelDb.Card<WindmillStrike>(),
            ModelDb.Card<Worship>(),
            ModelDb.Card<WreathOfFlame>(),
            ModelDb.Card<Alpha>(),
            ModelDb.Card<Blasphemy>(),
            ModelDb.Card<Brilliance>(),
            ModelDb.Card<ConjureBlade>(),
            ModelDb.Card<DeusExMachina>(),
            ModelDb.Card<DevaForm>(),
            ModelDb.Card<Devotion>(),
            ModelDb.Card<Establishment>(),
            ModelDb.Card<Judgment>(),
            ModelDb.Card<LessonLearned>(),
            ModelDb.Card<MasterReality>(),
            ModelDb.Card<Omniscience>(),
            ModelDb.Card<Ragnarok>(),
            ModelDb.Card<ScrawlWatcher>(),
            ModelDb.Card<SpiritShield>(),
            ModelDb.Card<Vault>(),
            ModelDb.Card<WishWatcher>(),

            ModelDb.Card<WeldingMallet>(),
            ModelDb.Card<Triplicator>(),
            ModelDb.Card<DistendedRectum>(),
            ModelDb.Card<TakingTheSmeg>(),
            ModelDb.Card<JusticeField>(),
            ModelDb.Card<Smeghead>(),
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
            ModelDb.Card<GarbageCannon>(),
            ModelDb.Card<WasteDisposal>(),
             ModelDb.Card<GarbagePod>(),
               ModelDb.Card<Scouter>(),




        ];
    }

    protected override IEnumerable<CardModel> FilterThroughEpochs(UnlockState unlockState, IEnumerable<CardModel> cards)
    {
        return cards.ToList();
    }
}