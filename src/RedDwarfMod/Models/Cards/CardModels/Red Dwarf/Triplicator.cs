using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace RedDwarfMod.Models.Cards;

public sealed class Triplicator() : RedDwarfCardModel(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self, RedDwarfCharacter.KRYTEN)
{
    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (CombatState == null) return;

        // Select a card from your hand pile
        var source = this;
        await CreatureCmd.TriggerAnim(source.Owner.Creature, "Cast", source.Owner.Character.CastAnimDelay);
        var prefs = new CardSelectorPrefs(source.SelectionScreenPrompt, 1)
        {
            PretendCardsCanBePlayed = true
        };
        var drawPile = PileType.Draw.GetPile(Owner).Cards.ToList();
        var card = (await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            drawPile,
            Owner,
            prefs
        )).FirstOrDefault();


        if (card != null)
        {
            var high = card.CreateClone();

            if (high != null)
            {
                foreach(string key in high.DynamicVars.Keys)
                {
                    high.DynamicVars[key].BaseValue += high.DynamicVars[key].BaseValue;
                }

                high.EnergyCost.SetThisTurn(0);
                // Shuffle it into draw pile (Random position)
                CardCmd.PreviewCardPileAdd(
                    await CardPileCmd.AddGeneratedCardToCombat(
                        high,
                        PileType.Hand,
                        false,
                        CardPilePosition.Top
                    )
                );  

            }

            var low = card.CreateClone();

            if (low != null)
            {
                foreach (string key in low.DynamicVars.Keys)
                {
                    low.DynamicVars[key].BaseValue = 0;
                }

                low.EnergyCost.SetThisCombat(3);

                // Shuffle it into draw pile (Random position)
                CardCmd.PreviewCardPileAdd(
                    await CardPileCmd.AddGeneratedCardToCombat(
                        low,
                        PileType.Hand,
                        false,
                        CardPilePosition.Top
                    )
                );

            }


        }
    }


    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}