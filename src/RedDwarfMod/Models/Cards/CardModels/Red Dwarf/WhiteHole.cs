
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using RedDwarfMod.Models.Powers;

namespace RedDwarfMod.Models.Cards;

public sealed class WhiteHole() : RedDwarfCardModel(3, CardType.Skill, CardRarity.Rare, TargetType.None, RedDwarfCharacter.CAT)
{


    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        ..base.CanonicalVars,
         new DynamicVar(Gold, 10m)
       
    ];

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust,
       
    ];

    protected override bool IsPlayable => Owner.Gold >= base.DynamicVars["Gold"].IntValue;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.LoseGold(base.DynamicVars[Gold].IntValue, base.Owner);
        PlayerCmd.EndTurn(Owner, false, SkipEnemyTurn);

    }

    public async Task SkipEnemyTurn()
    {
   
        GiveSingleTurnRetain();

        for (int i = CombatState.Enemies.Count - 1; i >= 0; i--)
        {
            Creature c = CombatState.Enemies[i];
            MonsterModel Monster = c.Monster;

            
            string nextState = Monster.MoveStateMachine.StateLog.Last().GetNextState(c, Monster.RunRng.MonsterAi);
            await CreatureCmd.Stun(c, nextState);
   
        }
    
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
        DynamicVars[Gold].UpgradeValueBy(-15);
    }
}