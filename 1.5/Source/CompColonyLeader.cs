using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ColonyLeadership;

public class CompColonyLeader : ThingComp
{
    private readonly List<StatModifier> _statFactors =
    [
        new StatModifier { stat = StatDefOf.SocialImpact, value = 1.25f },
        new StatModifier { stat = StatDefOf.TradePriceImprovement, value = 1.25f }
    ];

    public override float GetStatFactor(StatDef stat)
    {
        var factor = base.GetStatFactor(stat);

        foreach (var statModifier in _statFactors)
        {
            if (statModifier.stat != stat) continue;
            factor *= statModifier.value;
            Log.Message($"Applying {statModifier.value} factor to {stat}");
        }

        return factor;
    }
}
