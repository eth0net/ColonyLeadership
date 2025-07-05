using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class CompProperties_ColonyLeader : CompProperties
{
    public readonly List<StatModifier> statFactors =
    [
        new StatModifier { stat = StatDefOf.NegotiationAbility, value = 1.25f },
        new StatModifier { stat = StatDefOf.SocialImpact, value = 1.25f }
    ];

    public CompProperties_ColonyLeader()
    {
        compClass = typeof(CompColonyLeader);
    }
}
