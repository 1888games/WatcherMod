using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Enchantments;

namespace RedDwarfMod.Models.Cards;

public sealed class Triplicator() : RedDwarfCardModel(3, CardType.Skill, CardRarity.Rare, TargetType.Self, RedDwarfCharacter.KRYTEN)
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
            PretendCardsCanBePlayed = true,
            
        };
        var drawPile = PileType.Draw.GetPile(Owner).Cards.ToList();
        var card = (await CardSelectCmd.FromSimpleGrid(
            choiceContext,
            drawPile,
            Owner,
            prefs
        )).FirstOrDefault();

        if (card is Triplicator) return;

        if (card != null)
        {
            var high = card.CreateClone();

            if (high != null)
            {
                foreach(string key in high.DynamicVars.Keys)
                {
                    high.DynamicVars[key].BaseValue += high.DynamicVars[key].BaseValue;
                }

                high.EnergyCost.SetThisCombat(0);
                high.AddKeyword(CardKeyword.Retain);
                high.AddKeyword(CardKeyword.Exhaust);
                high.ExhaustOnNextPlay = true;
                CardCmd.Enchant<Glam>( high, 2);
                
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

                low.EnergyCost.SetThisCombat(2);
                low.AddKeyword(CardKeyword.Exhaust);
                low.AddKeyword(CardKeyword.Retain);
                low.ExhaustOnNextPlay = true;
                await CardCmd.Afflict<Hexed>( low, 1);

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