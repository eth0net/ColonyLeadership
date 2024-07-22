using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[HarmonyPatch(typeof(ExpectationsUtility), nameof(ExpectationsUtility.CurrentExpectationFor), typeof(Pawn))]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class Harmony_ExpectationsUtility_CurrentExpectationFor_Pawn
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static void Postfix(Pawn p, ref ExpectationDef __result)
    {
        // only consider player pawns
        if (p == null || p.Faction != Faction.OfPlayer || p.IsPrisonerOfColony) return;

        // don't change expectations for royalty or ideo roles
        if (__result.forRoles) return;

        // only change expectations for the leader
        var comp = Current.Game.GetComponent<GameComponent_ColonyLeadership>();
        if (comp.Leader != p) return;

        // get the next expectation in order, if it exists
        var leaderExpectation = ExpectationsUtility.ExpectationForOrder(__result.order + 1);
        if (leaderExpectation != null) __result = leaderExpectation;
    }
}
