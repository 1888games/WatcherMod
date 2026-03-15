
using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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

    protected override IEnumerable<DynamicVar> CanonicalVars =>
  [
      ..base.CanonicalVars,
         new DynamicVar("Gold", 50m)

  ];



    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.LoseGold(base.DynamicVars["Gold"].IntValue, base.Owner);

        RoomType roomType = RunState.CurrentRoom.RoomType;

        AbstractRoom currentRoom = RunState.CurrentRoom;


        SfxCmd.Play("event:/sfx/enemy/enemy_attacks/living_fog/living_fog_summon");

        await currentRoom.Exit(RunState);

        if (NGame.Instance != null)
        {
            NGame.Instance?.ScreenShakeTrauma(MegaCrit.Sts2.Core.Nodes.Vfx.Utilities.ShakeStrength.TooMuch);
            await NGame.Instance.Transition.RoomFadeOut();
        }
       

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
        DynamicVars["Gold"].UpgradeValueBy(-25);
    }
}
