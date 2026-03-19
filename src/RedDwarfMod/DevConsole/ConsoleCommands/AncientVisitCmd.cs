using MegaCrit.Sts2.Core.DevConsole;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Map;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Saves;

namespace RedDwarfMod.DevConsole.ConsoleCommands;

public class AncientVisitCmd : AbstractConsoleCmd
{
    public override string CmdName => "ancientvisit";
    public override string Args => "<id:string> <visit:int>";
    public override string Description => "Opens an ancient event at a specific visit index";
    public override bool IsNetworked => false;

    public override CmdResult Process(Player? issuingPlayer, string[] args)
    {
        if (issuingPlayer == null)
            return new CmdResult(false, "No player.");
        if (args.Length < 2)
            return new CmdResult(false, "Usage: ancientvisit <ancient_id> <visit_index> [character_id]");
        if (!int.TryParse(args[1], out var visitIndex) || visitIndex < 0)
            return new CmdResult(false, "Visit index must be a non-negative integer.");

        var modelId = new ModelId(ModelDb.GetCategory(typeof(EventModel)), args[0].ToUpperInvariant());
        var eventModel = ModelDb.GetByIdOrNull<EventModel>(modelId);
        if (eventModel == null)
            return new CmdResult(false, $"Unknown ancient: {args[0]}");
        if (eventModel is not AncientEventModel ancient)
            return new CmdResult(false, $"{args[0]} is not an ancient event.");

        ModelId characterId;
        if (args.Length >= 3)
        {
            var charModelId = new ModelId(ModelDb.GetCategory(typeof(CharacterModel)), args[2].ToUpperInvariant());
            if (ModelDb.GetByIdOrNull<CharacterModel>(charModelId) == null)
                return new CmdResult(false, $"Unknown character: {args[2]}");
            characterId = charModelId;
        }
        else
        {
            characterId = issuingPlayer.Character.Id;
        }

        var ancientStats = SaveManager.Instance.Progress.GetOrCreateAncientStats(modelId);
        var charStats = ancientStats.CharStats.FirstOrDefault(c => c.Character == characterId);
        if (charStats == null)
        {
            charStats = new AncientCharacterStats { Character = characterId };
            ancientStats.CharStats.Add(charStats);
        }

        var previousWins = charStats.Wins;
        var previousLosses = charStats.Losses;
        charStats.Wins = visitIndex;
        charStats.Losses = 0;

        issuingPlayer.RunState.AppendToMapPointHistory(MapPointType.Ancient, RoomType.Event, modelId);
        RunManager.Instance.EnterRoom(new EventRoom(eventModel));

        return new CmdResult(true,
            $"Opened {modelId.Entry} at visit {visitIndex} as {characterId.Entry} (was {previousWins + previousLosses})");
    }

    public override CompletionResult GetArgumentCompletions(Player? player, string[] args)
    {
        if (args.Length <= 1)
            return CompleteArgument(ModelDb.AllAncients.Select(a => a.Id.Entry).ToList(),
                Array.Empty<string>(), args.FirstOrDefault() ?? "");

        if (args.Length == 3)
            return CompleteArgument(ModelDb.AllCharacters.Select(c => c.Id.Entry).ToList(),
                args.Take(2).ToArray(), args[2]);

        return base.GetArgumentCompletions(player, args);
    }
}