using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace ColonyLeadership;

[HarmonyPatch(typeof(CharacterCardUtility), nameof(CharacterCardUtility.DrawCharacterCard))]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class Harmony_CharacterCardUtility_DrawCharacterCard
{
    private static readonly MethodInfo DrawTextureInfo =
        AccessTools.Method(typeof(GUI), nameof(GUI.DrawTexture), [typeof(Rect), typeof(Texture)]);

    private static readonly MethodInfo DrawLeaderButtonInfo =
        AccessTools.Method(typeof(Harmony_CharacterCardUtility_DrawCharacterCard), nameof(DrawLeaderButton));

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        var newCodes = new List<CodeInstruction>();

        for (var i = 0; i < codes.Count; i++)
        {
            var current = codes[i];

            if (i > 0 && codes[i - 1].Calls(DrawTextureInfo) && current.opcode == OpCodes.Ldloca_S)
            {
                // Load the current x position, subtract 40 and store back
                newCodes.Add(new CodeInstruction(OpCodes.Ldloc_S, 20));
                newCodes.Add(new CodeInstruction(OpCodes.Ldc_R4, 40f));
                newCodes.Add(new CodeInstruction(OpCodes.Sub));
                newCodes.Add(new CodeInstruction(OpCodes.Stloc_S, 20));

                // Load pawn and adjusted x position
                newCodes.Add(new CodeInstruction(OpCodes.Ldarg_1).MoveLabelsFrom(current));
                newCodes.Add(new CodeInstruction(OpCodes.Ldloc_S, 20));

                // Call DrawLeaderButton
                newCodes.Add(new CodeInstruction(OpCodes.Call, DrawLeaderButtonInfo));
            }

            // Add the original instruction
            newCodes.Add(current);
        }

#if DEBUG
        Log.Warning("Original codes:\n\n" + string.Join("\n", codes.Select(code => code.ToString())));
        Log.Warning("Transpiler finished:\n\n" + string.Join("\n", newCodes.Select(code => code.ToString())));
#endif

        return newCodes;
    }

    // todo: investigate weird behavior with Dialog_CreateStartingPawns
    public static void DrawLeaderButton(Pawn pawn, float x)
    {
#if DEBUG
        Log.Warning("Drawing leader button for " + pawn.Name);
#endif
        var rect = new Rect(x, 0f, 30f, 30f);
        var comp = Current.Game.GetComponent<GameComponent_ColonyLeadership>();
        var leader = comp.Leader;
        if (leader != pawn)
        {
#if DEBUG
            Log.Warning("Showing make leader button for " + pawn.Name);
#endif
            TooltipHandler.TipRegionByKey(rect, "ColonyLeadership.MakeLeader");
            if (Widgets.ButtonImage(rect, Icons.Triangle)) comp.Leader = pawn;
        }
        else
        {
#if DEBUG
            Log.Warning("Showing leader texture for " + pawn.Name);
#endif
            TooltipHandler.TipRegionByKey(rect, "ColonyLeadership.Leader");
            GUI.DrawTexture(rect, Icons.Star);
        }
    }
}
