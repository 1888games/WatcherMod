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

    //protected override IEnumerable<DynamicVar> CanonicalVars
    //{
    //    get
    //    {
    //        DynamicVar? powerVar = Character switch
    //        {
    //            RedDwarfCharacter.RIMMER => new PowerVar<RimmerPower>(1m),
    //            RedDwarfCharacter.CAT => new PowerVar<CatPower>(1m),
    //            RedDwarfCharacter.LISTER => new PowerVar<ListerPower>(1m),
    //            RedDwarfCharacter.KRYTEN => new PowerVar<KrytenPower>(1m),
    //            RedDwarfCharacter.KOCHANSKI => new PowerVar<KochanskiPower>(1m),

    //            _ => null
    //        };


    //        return powerVar is null
    //         ? base.CanonicalVars
    //         : [.. base.CanonicalVars, powerVar];
        
    //    }
    //}



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