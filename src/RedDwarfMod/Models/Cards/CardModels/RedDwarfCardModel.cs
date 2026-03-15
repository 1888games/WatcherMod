using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public enum RedDwarfCharacter { NONE, LISTER, CAT, RIMMER, KRYTEN, ACE, KOCHANSKI};

public abstract class RedDwarfCardModel : CardModel {

    public RedDwarfCharacter Character = RedDwarfCharacter.NONE;

    public const string Scrap = "ScrapPower";
    public const string Gold = "Gold";
    public const string Sustenance = "SustenancePower";
    public const string SmegDamage = "SmegDamage";
    public const string Smeg = "SmegPower";
    public const string Cowed = "CowedPower";
    public const string Mirror = "MirrorPower";
    public const string Radiation = "RadiationPower";

    public virtual int ScrapRequiredToPlay => 0;

    protected override bool IsPlayable =>
    base.IsPlayable &&
    (
        ScrapRequiredToPlay == 0 ||
        (Owner.Creature.HasPower<ScrapPower>() &&
         Owner.Creature.GetPower<ScrapPower>().Amount >= ScrapRequiredToPlay)
    );

    public RedDwarfCardModel(

    int canonicalEnergyCost,
    CardType type,
    CardRarity rarity,
    TargetType targetType,
    RedDwarfCharacter character = RedDwarfCharacter.NONE,
    bool shouldShowInCardLibrary = true)
    : base(canonicalEnergyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
        Character = character;

        switch (character)
        {
            case RedDwarfCharacter.RIMMER:

                CanonicalVars.AddItem(new PowerVar<RimmerPower>(1m));
                break;

            case RedDwarfCharacter.LISTER:

                CanonicalVars.AddItem(new PowerVar<ListerPower>(1m));
                break;

            case RedDwarfCharacter.CAT:

                CanonicalVars.AddItem(new PowerVar<CatPower>(1m));
                break;

            case RedDwarfCharacter.KOCHANSKI:

                CanonicalVars.AddItem(new PowerVar<KochanskiPower>(1m));
                break;

            case RedDwarfCharacter.KRYTEN:

                CanonicalVars.AddItem(new PowerVar<KrytenPower>(1m));
                break;


            default:
                break;
        }
    }




    protected override IEnumerable<IHoverTip> ExtraHoverTips
    {
        get
        {
            IHoverTip? hoverTip = Character switch
            {
                RedDwarfCharacter.RIMMER => HoverTipFactory.FromPower<RimmerPower>(),
                RedDwarfCharacter.CAT => HoverTipFactory.FromPower<CatPower>(),
                RedDwarfCharacter.LISTER => HoverTipFactory.FromPower<ListerPower>(),
                RedDwarfCharacter.KRYTEN => HoverTipFactory.FromPower<KrytenPower>(),
                RedDwarfCharacter.KOCHANSKI => HoverTipFactory.FromPower<KochanskiPower>(),

                _ => null
            };


            return hoverTip is null
             ? base.ExtraHoverTips
             : [.. base.ExtraHoverTips, hoverTip];

        }
    }

    public virtual Task OnStanceChanged(Creature creature)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnScryed(Player player, int amount)
    {
        return Task.CompletedTask;
    }
}