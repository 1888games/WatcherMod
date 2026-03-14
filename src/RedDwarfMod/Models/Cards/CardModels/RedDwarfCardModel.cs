using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
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




            default:
                break;
        }
    }

    protected override IEnumerable<DynamicVar> CanonicalVars =>
   [
       ..base.CanonicalVars,
        new PowerVar<RimmerPower>(1m),
    ];



    public virtual Task OnStanceChanged(Creature creature)
    {
        return Task.CompletedTask;
    }

    public virtual Task OnScryed(Player player, int amount)
    {
        return Task.CompletedTask;
    }
}