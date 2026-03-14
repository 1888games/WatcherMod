
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;

namespace RedDwarfMod.Models.Cards;

public sealed class DimensionJump() : RedDwarfCardModel(3, CardType.Skill, CardRarity.Rare, TargetType.None, RedDwarfCharacter.CAT)
{


    public override HashSet<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        RoomType roomType = RunState.CurrentRoom.RoomType;

        AbstractRoom currentRoom = RunState.CurrentRoom;

        NGame.Instance.ScreenShakeTrauma(MegaCrit.Sts2.Core.Nodes.Vfx.Utilities.ShakeStrength.Strong);

        
        SfxCmd.Play("event:/sfx/enemy/enemy_attacks/living_fog/living_fog_summon");

        await currentRoom.Exit(RunState);
        await NGame.Instance.Transition.RoomFadeOut();

        if (roomType == RoomType.Boss)
        {
            NCombatRoom.Instance?.CombatVfxContainer.AddChildSafely(NThoughtBubbleVfx.Create("This cannot be used in boss encounters!", Owner.Creature, 2.0));

            return;
        }

        EncounterModel encounter = RunState.Act.PullNextEncounter(roomType).ToMutable();

       

        AbstractRoom newRoom = new CombatRoom(encounter, RunState);

        await Task.Delay(250);
     

        await RunManager.Instance.EnterRoom(newRoom);
        await NGame.Instance.Transition.RoomFadeIn();



    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Retain);
    }
}