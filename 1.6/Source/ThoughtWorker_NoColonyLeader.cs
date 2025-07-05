using System.Diagnostics.CodeAnalysis;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class ThoughtWorker_NoColonyLeader : ThoughtWorker
{
    public override ThoughtState CurrentStateInternal(Pawn p)
    {
        if (p.Faction is { IsPlayer: false }) return false;

        var comp = Current.Game.GetComponent<GameComponent_ColonyLeadership>();

        return comp.NoLeaderThoughtActive;
    }
}
