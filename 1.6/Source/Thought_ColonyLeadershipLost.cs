using System.Diagnostics.CodeAnalysis;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Thought_ColonyLeadershipLost : Thought_Memory
{
    public override bool ShouldDiscard
    {
        get
        {
            var leader = Current.Game.GetComponent<GameComponent_ColonyLeadership>().Leader;
            return leader == pawn || base.ShouldDiscard;
        }
    }
}
