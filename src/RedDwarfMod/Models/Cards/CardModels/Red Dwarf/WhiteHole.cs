
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;

namespace RedDwarfMod.Models.Cards;

public sealed class WhiteHole() : RedDwarfCardModel(3, CardType.Skill, CardRarity.Rare, TargetType.None, RedDwarfCharacter.CAT)
{

    private bool _hasExtraTurn;

    List<Creature> enemies = new List<Creature>();

    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    public override bool ShouldTakeExtraTurn(Player player)
    {
        return _hasExtraTurn && player == Owner;
    }

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        
        PlayerCmd.EndTurn(Owner, false, SkipEnemyTurn);

        return Task.CompletedTask;
    }

    public async Task SkipEnemyTurn()
    {
        enemies = new List<Creature>();

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
    }
}