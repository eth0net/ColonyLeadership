using System.Diagnostics.CodeAnalysis;
using RimWorld;
using Verse;

namespace ColonyLeadership;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class GameComponent_ColonyLeadership(Game game) : GameComponent
{
    private const int TicksWithoutLeaderForThought = GenDate.TicksPerQuadrum;

    private readonly TickManager _tickManager = game.tickManager;

    private int _lastTickWithActiveLeader;

    private Pawn _leader;

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

    private bool HasActiveLeader => _leader is { Dead: false, Downed: false, IsPrisoner: false, IsSlave: false };

    public bool ThoughtActive => _tickManager.TicksGame - _lastTickWithActiveLeader >= TicksWithoutLeaderForThought;

    public override void GameComponentTick()
    {
        base.GameComponentTick();

        // Limit to every 60 ticks to reduce performance impact
        // Offset by a few ticks as 60 ticks is probably popular
        if (!GenTicks.IsTickInterval(5, 60)) return;

        // If there is an active leader, update the last tick with an active leader
        if (HasActiveLeader) _lastTickWithActiveLeader = _tickManager.TicksGame;
    }

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_References.Look(ref _leader, "leader");
    }
}
