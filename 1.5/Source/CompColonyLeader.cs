using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class CompColonyLeader : ThingComp
{
    public CompColonyLeader()
    {
        props = new CompProperties_ColonyLeader();
    }

    public CompProperties_ColonyLeader Props => (CompProperties_ColonyLeader)props;

    public IEnumerable<StatModifier> StatFactors => Props.statFactors;

    public override float GetStatFactor(StatDef stat)
    {
        var factor = base.GetStatFactor(stat);

        foreach (var statFactor in StatFactors)
        {
            if (statFactor.stat != stat) continue;
            factor *= statFactor.value;
        }

        return factor;
    }

    public override void GetStatsExplanation(StatDef stat, StringBuilder sb)
    {
        base.GetStatsExplanation(stat, sb);
        foreach (var statFactor in StatFactors)
        {
            if (statFactor.stat != stat) continue;
            var prefix = "ColonyLeadership.Leader".Translate();
            var factor = statFactor.value.ToStringByStyle(stat.toStringStyle, ToStringNumberSense.Factor);
            sb.AppendLine($"{prefix}: {factor}");
        }
    }
}

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class CompProperties_ColonyLeader : CompProperties
{
    public readonly List<StatModifier> statFactors =
    [
        new StatModifier { stat = StatDefOf.SocialImpact, value = 1.25f },
        new StatModifier { stat = StatDefOf.TradePriceImprovement, value = 1.25f }
    ];

    public CompProperties_ColonyLeader()
    {
        compClass = typeof(CompColonyLeader);
    }
}
