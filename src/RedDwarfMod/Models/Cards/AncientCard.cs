using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace RedDwarfMod.Models.Cards;

public sealed class AncientCard() : CardModel(1, CardType.Skill, CardRarity.Ancient, TargetType.Self)
{
    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];


    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }


    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}