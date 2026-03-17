using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace RedDwarfMod.Models.Cards;

public sealed class Scouter() : RedDwarfCardModel(1, CardType.Skill, CardRarity.Rare, TargetType.Self, RedDwarfCharacter.CAT)
{
    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override bool IsPlayable => (CardPile.GetCards(base.Owner, PileType.Draw).Count() + CardPile.GetCards(base.Owner, PileType.Discard).Count()) > 0;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {

        // Select a card from your hand pile
        var source = this;
        await CreatureCmd.TriggerAnim(source.Owner.Creature, "Cast", source.Owner.Character.CastAnimDelay);
        var prefs = new CardSelectorPrefs(source.SelectionScreenPrompt, 1)
        {
            PretendCardsCanBePlayed = true,

        };
        var drawPile = PileType.Draw.GetPile(Owner).Cards.ToList();
        var discardPile = PileType.Discard.GetPile(Owner).Cards.ToList();

        drawPile.AddRange(discardPile);

        var card = (await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            drawPile,
            Owner,
            prefs
        )).FirstOrDefault();




        if (card != null)
        {
            await CardPileCmd.Add(card, PileType.Hand);
        }
    }


    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}