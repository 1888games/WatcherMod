using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using RedDwarfMod.Models.Cards;
using RedDwarfMod.Models.Characters;
using System.Reflection;


[ModInitializer("Initialize")]
public class ModEntry
{
    public static void Initialize()
    {
        var harmony = new Harmony("watchermod.patch");
        Log.Info("WatcherMod");

        var assembly = Assembly.GetExecutingAssembly();
        ScriptManagerBridge.LookupScriptsInAssembly(assembly);

        //ProgressSaveManagerCustomCharPatch.Apply(harmony);
        harmony.PatchAll();
    }
}

[HarmonyPatch(typeof(TokenCardPool), "GenerateAllCards")]
public static class TokenCardPoolPatch
{
    // Postfix runs after the original method
    private static void Postfix(ref CardModel[] __result)
    {
        // Add custom cards to the existing pool
        var extraCards = new CardModel[]
        {
            ModelDb.Card<Beta>(),
            ModelDb.Card<Omega>(),
            ModelDb.Card<Insight>(),
            ModelDb.Card<Miracle>(),
            ModelDb.Card<BecomeAlmighty>(),
            ModelDb.Card<Expunger>(),
            ModelDb.Card<FameAndFortune>(),
            ModelDb.Card<LiveForever>(),
            ModelDb.Card<Safety>(),
            ModelDb.Card<Smite>(),
            ModelDb.Card<ThroughViolence>()
        };

        // Merge old and new cards
        __result = __result.Concat(extraCards).ToArray();
    }
}


[HarmonyPatch(typeof(ModelDb), "AllCharacters", MethodType.Getter)]
[HarmonyPriority(Priority.First)]
public class ModelDbAllCharactersPatch
{
    private static void Postfix(ref IEnumerable<CharacterModel> __result)
    {
        // Add Watcher to the list of all characters
        var charactersList = __result.ToList();
        charactersList.Add(ModelDb.Character<Watcher>());


        __result = charactersList;

        typeof(ModelDb).GetField("_allCharacterCardPools", BindingFlags.Static | BindingFlags.NonPublic)
            ?.SetValue(null, null);
        typeof(ModelDb).GetField("_allCards", BindingFlags.Static | BindingFlags.NonPublic)?.SetValue(null, null);
    }
}