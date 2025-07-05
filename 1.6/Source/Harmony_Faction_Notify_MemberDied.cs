using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[HarmonyPatch(typeof(Faction), nameof(Faction.Notify_MemberDied))]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public static class Harmony_Faction_Notify_MemberDied
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static void Postfix(Faction __instance, Pawn member, DamageInfo? dinfo)
    {
        if (member == null || member.Faction != __instance) return;

        var comp = Current.Game.GetComponent<GameComponent_ColonyLeadership>();
        if (member != comp.Leader) return;

        comp.Notify_LeaderDied();
    }
}
