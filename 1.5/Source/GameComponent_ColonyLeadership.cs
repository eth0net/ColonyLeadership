using System.Diagnostics.CodeAnalysis;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GameComponent_ColonyLeadership : GameComponent
{
    private Pawn _leader;

    public GameComponent_ColonyLeadership(Game game)
    {
    }

    public Pawn Leader
    {
        get => _leader;
        set
        {
            _leader?.needs.mood.thoughts.memories.TryGainMemory(
                ThoughtMaker.MakeThought(ThoughtDefOf.ColonyLeadershipLost, 0));
            _leader = value;
            _leader?.needs.mood.thoughts.memories.TryGainMemory(
                ThoughtMaker.MakeThought(ThoughtDefOf.ColonyLeadershipGained, 0));
        }
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_References.Look(ref _leader, "leader");
    }
}
