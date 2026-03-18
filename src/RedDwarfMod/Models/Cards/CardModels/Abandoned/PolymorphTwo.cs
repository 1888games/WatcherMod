using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.MonsterMoves.MonsterMoveStateMachine;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;


public sealed class PolymorphTwo() : RedDwarfCardModel(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, RedDwarfCharacter.CAT)
{


    public PowerModel? Buff;
    public PowerModel? Debuff;

    public List<Func<IReadOnlyList<Creature>, Task>> moveQueue = new();

    public List<MoveState> MoveStates = new List<MoveState>();

    public MonsterModel? StolenMonster;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
   [
       ..base.CanonicalVars,
        new DamageVar(0m, ValueProp.Move),
        new BlockVar(0m, ValueProp.Move),
        new RepeatVar(0)
       
   ];


    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target);

        await StolenMonster.NextMove.PerformMove(new Creature[] { cardPlay.Target });
        
        if (DynamicVars.Damage.BaseValue > 0)
        {
            await DamageCmd.Attack(DynamicVars.Damage.BaseValue).FromCard(this).Targeting(cardPlay.Target).WithHitCount(DynamicVars.Repeat.IntValue)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

        }
     

    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);

    }
}
