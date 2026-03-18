
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace RedDwarfMod.Models.Cards;

public sealed class PolymorphOne() : RedDwarfCardModel(3, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy, RedDwarfCharacter.CAT)
{


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
       
    ];

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust,
        CardKeyword.Retain,
        CardKeyword.Innate
    ];



    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
         ..base.ExtraHoverTips,
      
    ];




    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        if (cardPlay.Target.Monster == null) return;

        //var intents = cardPlay.Target.Monster.NextMove.Intents;

        //MonsterModel mm = cardPlay.Target.Monster;

        //MoveState ms = cardPlay.Target.Monster.NextMove;

        var type = cardPlay.Target.Monster.GetType();
        var clone = (MonsterModel)FormatterServices.GetUninitializedObject(cardPlay.Target.Monster.GetType());

        // var t = type;
        // while (t != null)
        // {
        // foreach (var field in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
        //     field.SetValue(clone, field.GetValue(cardPlay.Target.Monster));
        // t = t.BaseType;
        // }

        //  clone.NextMove = cardPlay.Target.Monster.NextMove;

        MoveState ms = new MoveState("COPY", cardPlay.Target.Monster.NextMove.PerformMove, new HiddenIntent());

        typeof(MonsterModel)
        .GetProperty(nameof(MonsterModel.NextMove))
         .SetValue(clone, ms);



        var createdCard = CombatState.CreateCard<PolymorphTwo>(Owner);

        createdCard.StolenMonster = clone;
       // clone.SetMoveImmediate(new MoveState("COPY", cardPlay.Target.Monster.NextMove.PerformMove, new HiddenIntent()),true);


        clone.Creature = Owner.Creature;
        //foreach (AbstractIntent intent in intents)
        //{


        //    switch (intent.IntentType)
        //    {
        //        case IntentType.Attack:

        //            AttackIntent a = (AttackIntent)intent;

        //            createdCard.DynamicVars.Damage.UpgradeValueBy(a.GetSingleDamage(new Creature[] { Owner.Creature }, cardPlay.Target.Monster.Creature));
        //            createdCard.DynamicVars.Repeat.UpgradeValueBy(a.Repeats);

        //            foundIntent = true;


        //            break;


        //        case IntentType.Defend:

        //            DefendIntent d = (DefendIntent)intent;

        //            createdCard.DynamicVars.Block.UpgradeValueBy(10m);
        //            foundIntent = true;


        //            break;


        //        case IntentType.Buff:

        //            BuffIntent b = (BuffIntent)intent;

        //            createdCard.DynamicVars.Block.UpgradeValueBy(10m);
        //            foundIntent = true;


        //            break;


        //        default:
        //            break;
        //    }


        //}

        //if (foundIntent == false) return;

        // Shuffle it into draw pile (Random position)
        CardCmd.PreviewCardPileAdd(
            await CardPileCmd.AddGeneratedCardToCombat(
                createdCard,
                PileType.Hand,
                false,
                CardPilePosition.Top
            )
        );

       // await PowerCmd.Apply<SmegPower>(
          //  cardPlay.Target,
           // DynamicVars[Smeg].IntValue,
           // Owner.Creature,
           // this
      //  );
       
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}