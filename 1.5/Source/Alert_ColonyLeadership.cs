using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Alert_ColonyLeadership : Alert
{
#if DEBUG
    private Pawn _factionLeader;
#endif

    private List<Pawn> Targets { get; set; } = [];

    private void GetTargets()
    {
        Targets.Clear();
        Targets = Find.CurrentMap.mapPawns.FreeColonistsSpawned
            .Where(pawn => !pawn.IsSlave)
            .ToList();
    }

    public override string GetLabel()
    {
        return "ColonyLeadership.NoLeader".Translate();
    }

    public override TaggedString GetExplanation()
    {
        return "ColonyLeadership.NoLeaderDesc".Translate();
    }

    public override AlertReport GetReport()
    {
        var leader = Current.Game.GetComponent<GameComponent_ColonyLeadership>().Leader;

        if (leader != null) return false;

#if DEBUG
        if (Faction.OfPlayer.leader != _factionLeader)
        {
            _factionLeader = Faction.OfPlayer.leader;
            Log.Warning("Player faction leader is " + _factionLeader.NameFullColored);
        }
#endif

        GetTargets();
        return AlertReport.CulpritsAre(Targets);
    }
}
