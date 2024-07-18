using System.Diagnostics.CodeAnalysis;
using RimWorld;

namespace ColonyLeadership;

[DefOf]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
public static class ThoughtDefOf
{
    public static ThoughtDef ColonyLeadershipGained;
    public static ThoughtDef ColonyLeadershipLost;
    public static ThoughtDef ColonyLeaderDied;
}
